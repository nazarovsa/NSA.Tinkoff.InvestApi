using Grpc.Core;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Contracts;

public sealed class InvestApiClient : IInvestApiClient
{
    public UsersService.UsersServiceClient UsersServiceClient { get; }
    public MarketDataStreamService.MarketDataStreamServiceClient MarketDataStreamServiceClient { get; }
    
    public InstrumentsService.InstrumentsServiceClient InstrumentsServiceClient { get; }

    public InvestApiClient(CallInvoker callInvoker)
    {
        if (callInvoker == null)
        {
            throw new ArgumentNullException(nameof(callInvoker));
        }

        UsersServiceClient = new UsersService.UsersServiceClient(callInvoker);
        MarketDataStreamServiceClient = new MarketDataStreamService.MarketDataStreamServiceClient(callInvoker);
        InstrumentsServiceClient = new InstrumentsService.InstrumentsServiceClient(callInvoker);
    }
}