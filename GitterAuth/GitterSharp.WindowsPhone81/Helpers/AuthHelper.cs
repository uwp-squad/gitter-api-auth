using GitterSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Authentication.Web;

namespace GitterSharp.Helpers
{
    internal static class AuthHelper
    {
        #region Properties

        /// <summary>
        /// Redirect URL when authenticate
        /// </summary>
        public static string RedirectUrl = "http://localhost";

        #endregion


        #region Methods

        public static async Task<string> RetrieveToken(WebAuthenticationResult result, string oauthKey, string oauthSecret)
        {
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                string code = GetCode(result.ResponseData);
                return await GetToken(code, oauthKey, oauthSecret);
            }
            if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                throw new ApiAuthException(WebAuthenticationStatus.ErrorHttp);
            }
            if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                throw new ApiAuthException(WebAuthenticationStatus.UserCancel);
            }

            return null;
        }

        private static string GetCode(string webAuthResultResponseData)
        {
            string[] splitResultResponse = webAuthResultResponseData.Split('&');
            string codeString = splitResultResponse.FirstOrDefault(value => value.Contains("code"));
            string[] splitCode = codeString.Split('=');
            return splitCode.Last();
        }

        private static async Task<string> GetToken(string code, string oauthKey, string oauthSecret)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://gitter.im");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", oauthKey),
                    new KeyValuePair<string, string>("client_secret", oauthSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUrl),
                    new KeyValuePair<string, string>("grant_type", "authorization_code")
                });

                var result = await httpClient.PostAsync("/login/oauth/token", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                JsonObject value = JsonValue.Parse(resultContent).GetObject();

                return value.GetNamedString("access_token");
            }
        }

        #endregion
    }
}