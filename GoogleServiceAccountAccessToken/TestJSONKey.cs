namespace GoogleServiceAccountAccessToken
{
    class TestJSONKey
    {
        public static string GetToken(string scopesSemicolonDelimited, string impersonate, out bool isError)
        {
            var scopes = scopesSemicolonDelimited.Split(';');
            var token = GoogleServiceAccount.GetAccessTokenFromJSONKey(
             "Keys/serviceaccount.json",
             impersonate,
             out isError,
             scopes);

            return token;

        }
    }
}