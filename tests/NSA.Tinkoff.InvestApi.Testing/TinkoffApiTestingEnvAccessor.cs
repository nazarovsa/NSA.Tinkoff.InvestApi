namespace NSA.Tinkoff.InvestApi.Testing;

public static class TinkoffApiTestingEnvAccessor
{
    public static string GetToken()
    {
        return GetValue("NSA_INVESTAPI_ACCESSTOKEN");
    }

    public static string GetAccountId()
    {
        return GetValue("NSA_INVESTAPI_ACCOUNTID");
    }

    private static string GetValue(string key)
    {
        var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

        if (string.IsNullOrWhiteSpace(value))
            value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);

        if (string.IsNullOrWhiteSpace(value))
            value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        return value;
    }
}
