using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterAuth.Exceptions
{
    /// <summary>
    /// Api Auth Exception designed using octokit.net example
    /// https://github.com/octokit/octokit.net/blob/1266ac0f3a366f033061d0c1cc0785bc3c9f5bd3/Octokit/Exceptions/ApiException.cs
    /// </summary>
    public class ApiAuthException : Exception
    {
        public ApiAuthException() { }

        public ApiAuthException(string message): base(message) { }
    }
}
