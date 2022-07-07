using Grpc.Core;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Contracts;

public sealed class InvestApiClient : IInvestApiClient
{
    public UsersService.UsersServiceClient UsersServiceClient { get; }

    public InstrumentsService.InstrumentsServiceClient InstrumentsServiceClient { get; }

    public OrdersService.OrdersServiceClient OrdersServiceClient { get; }

    public MarketDataStreamService.MarketDataStreamServiceClient MarketDataStreamServiceClient { get; }
    
    public OrdersStreamService.OrdersStreamServiceClient OrdersStreamServiceClient { get; }

    public InvestApiClient(CallInvoker callInvoker)
    {
        if (callInvoker == null)
        {
            throw new ArgumentNullException(nameof(callInvoker));
        }

        UsersServiceClient = new UsersService.UsersServiceClient(callInvoker);
        InstrumentsServiceClient = new InstrumentsService.InstrumentsServiceClient(callInvoker);
        OrdersServiceClient = new OrdersService.OrdersServiceClient(callInvoker);
        MarketDataStreamServiceClient = new MarketDataStreamService.MarketDataStreamServiceClient(callInvoker);
        OrdersStreamServiceClient = new OrdersStreamService.OrdersStreamServiceClient(callInvoker);
    }
}