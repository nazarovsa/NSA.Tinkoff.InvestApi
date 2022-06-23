using Microsoft.Extensions.DependencyInjection;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;

namespace NSA.Tinkoff.InvestApi.Testing;

public static class TinkoffApiClientProvider
{
    // TODO: Understand how handle client creation without extension method and refactor that.
    public static IInvestApiClient GetInstance()
    {
        var options = new InvestApiOptions
        {
            AccessToken = AccessTokenAccessor.GetFromEnv(),
        };

        var services = new ServiceCollection();
        services.AddInvestApiClient("Testing", options);

        var sp = services.BuildServiceProvider();
        
        return sp.GetRequiredService<InvestApiClient>();
    }
}