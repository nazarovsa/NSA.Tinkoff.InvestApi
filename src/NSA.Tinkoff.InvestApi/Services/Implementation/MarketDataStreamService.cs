using Grpc.Core;
using Grpc.Core.Utils;
using NSA.Tinkoff.InvestApi.Contracts;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class MarketDataStreamService : IMarketDataStreamService, IDisposable
{
    private readonly AsyncDuplexStreamingCall<MarketDataRequest, MarketDataResponse> _stream;
    
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

        return SendAsync(new[] { request }, cancellationToken);
    }

    public Task SendAsync(MarketDataRequest[] requests, CancellationToken cancellationToken = default)
    {
        if (requests == null)
        {
            throw new ArgumentNullException(nameof(requests));
        }

        return _stream.RequestStream.WriteAllAsync(requests);
    }

    public async Task<MarketDataResponse?> ReadAsync(CancellationToken cancellationToken = default)
    {
        if (await _stream.ResponseStream.MoveNext())
        {
            return _stream.ResponseStream.Current;
        }

        return null;
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}