using System.Net.Http.Headers;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;

namespace NSA.Tinkoff.InvestApi.Testing;

public static class TinkoffApiClientProvider
{
    public static ITinkoffApiClient GetInstance()
    {
        var options = new InvestApiOptions
        {
            AccessToken = AccessTokenAccessor.GetFromEnv(),
        };

        var httpClient = new HttpClient
        {
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken) }
        };

        return new TinkoffApiClient(httpClient);
    }
}