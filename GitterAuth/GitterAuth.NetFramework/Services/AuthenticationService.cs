using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitterAuth.Helpers;
using System.Windows.Forms;
using GitterAuth.Controls;

namespace GitterAuth.Services
{
    /// <summary>
    /// Service used to finalize the authentication
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
        /// <returns>Returns the generated token (null if an error occured)</returns>
        public string Login(string oauthKey, string oauthSecret)
        {
            try
            {
                string token = string.Empty;
                _oauthKey = oauthKey;
                _oauthSecret = oauthSecret;

                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={AuthHelper.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(AuthHelper.RedirectUrl);

                var browser = new BrowserWindow(startUrl);
                browser.CodeGenerated += (string code) =>
                {
                    token = AuthHelper.GetToken(code, _oauthKey, _oauthSecret);
                };

                Application.Run(browser);

                return token;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
