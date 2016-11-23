using GitterAuth.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GitterAuth.Helpers
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

        public static string GetCode(string webAuthResultResponseData)
        {
            string[] splitResultResponse = webAuthResultResponseData.Split('&');

            string errorString = splitResultResponse.FirstOrDefault(value => value.Contains("error"));
            if (!string.IsNullOrWhiteSpace(errorString))
            {
                string[] splitCode = errorString.Split('=');
                throw new ApiAuthException($"An error occured: {splitCode.Last()}.");
            }

            string codeString = splitResultResponse.FirstOrDefault(value => value.Contains("code"));
            if (!string.IsNullOrWhiteSpace(codeString))
            {
                string[] splitCode = codeString.Split('=');
                return splitCode.Last();
            }

            throw new ApiAuthException("An error occured.");
        }

        public static string GetToken(string code, string oauthKey, string oauthSecret)
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

                var result = httpClient.PostAsync("/login/oauth/token", content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                
                var value = (dynamic)JsonConvert.DeserializeObject(resultContent);
                return value.access_token;
            }
        }

        #endregion
    }
}
