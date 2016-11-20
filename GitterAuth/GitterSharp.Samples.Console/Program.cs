using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GitterSharp.Services;

namespace GitterSharp.Samples.ConsoleApp
{
    public class Program
    {
        private static string _oauthKey = "<key>";
        private static string _oauthSecret = "<secret>";

        [STAThread]
        static void Main(string[] args)
        {
            var authenticationService = new AuthenticationService();
            string token = authenticationService.Login(_oauthKey, _oauthSecret);

            // Now you can use the token with the Gitter Api

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
