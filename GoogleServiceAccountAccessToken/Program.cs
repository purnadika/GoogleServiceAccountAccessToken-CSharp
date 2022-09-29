using static System.Console;
using System.Windows.Forms;
using System;

namespace GoogleServiceAccountAccessToken
{
    class Program
    {
        const string SCOPE_INFO = "Built from https://github.com/purnadika/GoogleServiceAccountAccessToken-CSharp\nScope information : https://developers.google.com/identity/protocols/oauth2/scopes \n";
        [STAThread]
        // Generate your own keys before running 
        static void Main(string[] args)
        {
            WriteLine(SCOPE_INFO);
            string scopes = string.Empty;
            bool finish = false;
            bool renew = false;
            string impersonateAs = string.Empty;
            while (!finish)
            {
                if (!renew)
                {
                    Write("Enter Scopes (Semicolon delimited if more than 1) : ");
                    scopes = ReadLine();
                    WriteLine("Impersonate as User (Optionally leave blank. Only required to access Google Workspace Admin)");
                    Write("Domain admin email : ");
                    impersonateAs = ReadLine();
                }
                bool isError;
                string result = TestJSONKey.GetToken(scopes, impersonateAs, out isError);
                string statusMessage;
                WriteLine("\n\n\n");
                if (isError)
                {
                    statusMessage = "Error : \n";
                }
                else
                {
                    statusMessage = "Token Result (Copied to Clipboard): \n";
                    Clipboard.SetText(result);
                }
                WriteLine($"{statusMessage}{result}");

                WriteLine("\n\n");
                WriteLine("Press 1 to Renew Previous Token Configuration");
                WriteLine("Press 2 to Generate new token (Input new scopes and impersonation)");
                WriteLine("Press Any Key to Exit Program");
                var choice = ReadKey();
                switch (choice.Key)
                {
                    case (ConsoleKey.NumPad1):
                    case (ConsoleKey.D1):
                        renew = true;
                        break;
                    case (ConsoleKey.NumPad2):
                    case (ConsoleKey.D2):
                        renew = false;
                        break;
                    default:
                        finish = true;
                        break;
                }
                Clear();
                WriteLine("Scope information : https://developers.google.com/identity/protocols/oauth2/scopes \n");
            }
        }
    }
}