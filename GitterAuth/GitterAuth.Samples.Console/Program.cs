using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GitterAuth.Services;

namespace GitterAuth.Samples.ConsoleApp
{
    public class Program
    {
        private static string _oauthKey = "0f3fc414587a8d31a1514e005fa157168ad8efdb";
        private static string _oauthSecret = "55c361ef1de79ffef1a49a1a0bff1a7a0140799c";

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
