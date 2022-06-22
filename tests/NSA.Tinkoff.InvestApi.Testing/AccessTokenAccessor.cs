namespace NSA.Tinkoff.InvestApi.Testing;

public static class AccessTokenAccessor
{
    public static string GetFromEnv()
    {
        var token = Environment.GetEnvironmentVariable("NSA_INVESTAPI_ACCESSTOKEN", EnvironmentVariableTarget.Process);
        
        if(string.IsNullOrWhiteSpace(token))
            token = Environment.GetEnvironmentVariable("NSA_INVESTAPI_ACCESSTOKEN", EnvironmentVariableTarget.User);
        
        if(string.IsNullOrWhiteSpace(token))
            token = Environment.GetEnvironmentVariable("NSA_INVESTAPI_ACCESSTOKEN", EnvironmentVariableTarget.Machine);
        
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentNullException(nameof(token));
        }

        return token;
    }
}