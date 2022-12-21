using Microsoft.Extensions.Hosting;
using NSA.Tinkoff.InvestApi.Samples.MarketDataStream;

var host = new HostBuilderFactory()
    .CreateHostBuilder(args)
    .Build();

await host.RunAsync();