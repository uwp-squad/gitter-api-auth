using GitterAuth.Helpers;
using System;
using System.Threading.Tasks;
#if WINDOWS_PHONE_APP || WINDOWS_APP
using Windows.Security.Authentication.Web;
#endif
#if WINDOWS_PHONE_APP
using Windows.ApplicationModel.Activation;
#endif

namespace GitterAuth.Services
{
    /// <summary>
    /// Service used to finalize the authentication using Web Authentication Broker on Windows (Phone) 8.1
    /// </summary>
    public class AuthenticationService
    {
        #region Fields

#if WINDOWS_PHONE_APP
        private static string _oauthKey { get; set; }
        private static string _oauthSecret { get; set; }
#endif

#if WINDOWS_APP
        private static string _token { get; set; }
#endif

        #endregion


        #region Methods

        /// <summary>
        /// Execute login process through OAuth2 authentication mechanism
        /// (https://developer.gitter.im/docs/authentication)
        /// </summary>
        /// <returns>true: login success / false: login failed / null: exception occured</returns>
        public async Task<bool?> LoginAsync(string oauthKey, string oauthSecret)
        {
            try
            {
#if WINDOWS_PHONE_APP
                _oauthKey = oauthKey;
                _oauthSecret = oauthSecret;
#endif

                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={AuthHelper.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(AuthHelper.RedirectUrl);

#if WINDOWS_PHONE_APP
                WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
                return await Task.FromResult<bool?>(null);
#endif
#if WINDOWS_APP
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                _token = await AuthHelper.RetrieveToken(webAuthenticationResult, oauthKey, oauthSecret);
                return !string.IsNullOrWhiteSpace(_token);
#endif
            }
            catch
            {
                return null;
            }
        }

#if WINDOWS_PHONE_APP

        /// <summary>
        /// Retrieve token to use Gitter Api methods that requires a connected user
        /// </summary>
        /// <param name="args">Args when the Continuation is over, using WebAuthenticationBroker</param>
        /// <returns>The token</returns>
        public async Task<string> RetrieveTokenAsync(WebAuthenticationBrokerContinuationEventArgs args)
        {
            try
            {
                return await AuthHelper.RetrieveToken(args.WebAuthenticationResult, _oauthKey, _oauthSecret);
            }
            catch
            {
            }
            finally
            {
                _oauthKey = string.Empty;
                _oauthSecret = string.Empty;
            }

            return null;
        }

#endif

#if WINDOWS_APP

        /// <summary>
        /// Retrieve token to use Gitter Api methods that requires a connected user
        /// </summary>
        /// <returns>The token</returns>
        public Task<string> RetrieveTokenAsync()
        {
            return Task.FromResult(_token);
        }

#endif

        #endregion
    }
}