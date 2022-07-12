using System.Threading;
using System.Threading.Tasks;
using NSA.Tinkoff.InvestApi.Services;
using NSA.Tinkoff.InvestApi.Testing;
using Tinkoff.InvestApi.V1;
using Xunit;
using MarketDataStreamService = NSA.Tinkoff.InvestApi.Services.MarketDataStreamService;

namespace NSA.Tinkoff.InvestApi.UnitTests;

public class MarketDataStreamServiceTests
{
    private readonly IMarketDataStreamService _marketDataStreamService;

    public MarketDataStreamServiceTests()
    {
        var client = TinkoffApiClientProvider.GetInstance();
        _marketDataStreamService = new MarketDataStreamService(client);
    }

    [Fact(Skip = "Non-repeatable.")]
    public async Task SubscribeAsync_ToLastPrice_Then_ReadAsync_ReturnsLastPrice()
    {
        var request = new[]
        {
            new MarketDataRequest
            {
                SubscribeLastPriceRequest = new SubscribeLastPriceRequest
                {
                    SubscriptionAction = SubscriptionAction.Subscribe,
                    Instruments =
                    {
                        new LastPriceInstrument
                        {
                            Figi = "BBG000BBJQV0",
                        }
                    }
                }
            }
        };

        // Act
        var cancellationToken = new CancellationToken();
        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(10 * 1000);
        
        await _marketDataStreamService.SendAsync(request, cts.Token);
        var subscribeLastPriceResponse = await _marketDataStreamService.ReadAsync(cts.Token);

        MarketDataResponse? lastPriceResponse = null;
        while (lastPriceResponse == null)
        {
            lastPriceResponse = await _marketDataStreamService.ReadAsync(cts.Token);

            await Task.Delay(1000, cts.Token);
        }

        // Assert
        Assert.NotNull(subscribeLastPriceResponse);
        Assert.Equal(MarketDataResponse.PayloadOneofCase.SubscribeLastPriceResponse,
            subscribeLastPriceResponse!.PayloadCase);

        Assert.NotNull(lastPriceResponse);
        Assert.Equal(MarketDataResponse.PayloadOneofCase.LastPrice,
            lastPriceResponse!.PayloadCase);
    }
}