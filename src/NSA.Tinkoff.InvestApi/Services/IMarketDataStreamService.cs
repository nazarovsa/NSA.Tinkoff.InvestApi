using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IMarketDataStreamService
{
    Task SendAsync(MarketDataRequest request, CancellationToken cancellationToken = default);
    
    Task SendAsync(MarketDataRequest[] requests, CancellationToken cancellationToken = default);

    Task<MarketDataResponse?> ReadAsync(CancellationToken cancellationToken = default);
}