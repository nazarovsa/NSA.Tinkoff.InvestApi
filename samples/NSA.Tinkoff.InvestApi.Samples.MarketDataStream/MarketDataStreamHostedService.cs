using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NSA.Tinkoff.InvestApi.Services;

namespace NSA.Tinkoff.InvestApi.Samples.MarketDataStream;

public sealed class MarketDataStreamHostedService : BackgroundService
{
    private readonly IMarketDataStreamService _marketDataStreamService;

    public MarketDataStreamHostedService(IMarketDataStreamService marketDataStreamService)
    {
        _marketDataStreamService = marketDataStreamService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var response in _marketDataStreamService.ReadAllAsync(stoppingToken))
        {
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
}