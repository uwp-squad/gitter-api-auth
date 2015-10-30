using System;
using Windows.Security.Authentication.Web;

namespace GitterSharp.Exceptions
{
    /// <summary>
    /// Api Auth Exception designed using octokit.net example
    /// https://github.com/octokit/octokit.net/blob/1266ac0f3a366f033061d0c1cc0785bc3c9f5bd3/Octokit/Exceptions/ApiException.cs
    /// </summary>
    public class ApiAuthException : Exception
    {
        #region Properties

        /// <summary>
        /// The HTTP status code associated with the repsonse
        /// </summary>
        public WebAuthenticationStatus AuthenticationStatus { get; private set; }

        #endregion


        #region Constructors

        public ApiAuthException() { }

        public ApiAuthException(WebAuthenticationStatus authenticationStatus)
        {
            AuthenticationStatus = authenticationStatus;
        }

        #endregion
    }
}