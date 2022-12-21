using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class MarketDataStreamService : IMarketDataStreamService, IDisposable
{
    public readonly AsyncDuplexStreamingCall<MarketDataRequest, MarketDataResponse> _stream;
    
    public MarketDataStreamService(IInvestApiClient client)
    {
        _stream = client.MarketDataStreamServiceClient.MarketDataStream();
    }

    public Task SendAsync(MarketDataRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return _stream.RequestStream.WriteAsync(request, cancellationToken);
    }

    public async Task<MarketDataResponse?> ReadAsync(CancellationToken cancellationToken = default)
    {
        if (await _stream.ResponseStream.MoveNext())
        {
            return _stream.ResponseStream.Current;
        }

        return null;
    }

    public IAsyncEnumerable<MarketDataResponse> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return _stream.ResponseStream.ReadAllAsync(cancellationToken);
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}