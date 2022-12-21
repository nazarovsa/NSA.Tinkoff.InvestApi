using Microsoft.Extensions.Hosting;
using NSA.Tinkoff.InvestApi.Services;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Samples.MarketDataStream;

public sealed class MarketDataStreamSubscriptionService : BackgroundService
{
    private readonly IMarketDataStreamService _marketDataStreamService;

    private const string SberFigi = "BBG004730N88";
    
    public MarketDataStreamSubscriptionService(IMarketDataStreamService marketDataStreamService)
    {
        _marketDataStreamService = marketDataStreamService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _marketDataStreamService.SendAsync(new MarketDataRequest
        {
            SubscribeOrderBookRequest = new SubscribeOrderBookRequest()
            {
                Instruments =
                {
                    new OrderBookInstrument()
                    {
                        Figi = SberFigi,
                        Depth = 10
                    }
                },
                SubscriptionAction = SubscriptionAction.Subscribe
            }
        }, stoppingToken);
    }
}