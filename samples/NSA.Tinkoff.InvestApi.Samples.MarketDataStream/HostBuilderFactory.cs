using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSA.Tinkoff.InvestApi.Options;
using NSA.Tinkoff.InvestApi.Testing;

namespace NSA.Tinkoff.InvestApi.Samples.MarketDataStream;

public class HostBuilderFactory
{
    public IHostBuilder CreateHostBuilder(string[] args, string? baseDirectory = null)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                var options = new InvestApiOptions()
                {
                    AccessToken = TinkoffApiTestingEnvAccessor.GetToken()
                };

                services.AddInvestApiClient(Assembly.GetEntryAssembly().GetName().Name, options);
                services.AddMarketDataStreamService();

                services.AddHostedService<MarketDataStreamSubscriptionService>();
                services.AddHostedService<MarketDataStreamHostedService>();
            });

        return builder;
    }
}