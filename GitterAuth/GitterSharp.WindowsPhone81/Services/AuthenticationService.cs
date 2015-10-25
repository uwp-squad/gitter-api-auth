using GitterSharp.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Security.Authentication.Web;

namespace GitterSharp.Services
{
    /// <summary>
    /// Service used to finalize the authentication using Web Authentication Broker on Windows Phone 8.1
    /// </summary>
    public class AuthenticationService
    {
        #region Fields

        private static string _oauthKey { get; set; }
        private static string _oauthSecret { get; set; }

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
                _oauthKey = oauthKey;
                _oauthSecret = oauthSecret;

                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={AuthHelper.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(AuthHelper.RedirectUrl);

                WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);

                return await Task.FromResult<bool?>(null);
            }
            catch
            {
                return null;
            }
        }

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

        #endregion
    }
}
