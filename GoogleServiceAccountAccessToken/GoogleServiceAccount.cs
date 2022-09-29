using Google.Apis.Auth.OAuth2;
using System;
using System.IO;

namespace GoogleServiceAccountAccessToken
{
    public class GoogleServiceAccount
    {
        public static string GetAccessTokenFromJSONKey(string jsonKeyFilePath, string impersonate, out bool isError, params string[] scopes)
        {
            string result = string.Empty;
            isError = false;
            using (var stream = new FileStream(jsonKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    if (string.IsNullOrEmpty(impersonate))
                    {
                        result = GoogleCredential
                            .FromStream(stream) // Loads key file
                            .CreateScoped(scopes) // Gathers scopes requested
                                                  //No Impersonate
                            .UnderlyingCredential // Gets the credentials
                            .GetAccessTokenForRequestAsync().Result; // Gets the Access Token
                    }
                    else
                    {
                        result = GoogleCredential
                            .FromStream(stream) // Loads key file
                            .CreateScoped(scopes) // Gathers scopes requested
                            .CreateWithUser(impersonate) //Impersonate
                            .UnderlyingCredential // Gets the credentials
                            .GetAccessTokenForRequestAsync().Result; // Gets the Access Token
                    }
                }
                catch (Exception ex)
                {
                    isError = true;
                    result = $"{ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}";
                }
            }
            return result;
        }
    }
}