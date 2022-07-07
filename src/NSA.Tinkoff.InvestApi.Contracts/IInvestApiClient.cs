using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Contracts;

public interface IInvestApiClient
{
    UsersService.UsersServiceClient UsersServiceClient { get; }

    InstrumentsService.InstrumentsServiceClient InstrumentsServiceClient { get; }

    OrdersService.OrdersServiceClient OrdersServiceClient { get; }
    
    MarketDataStreamService.MarketDataStreamServiceClient MarketDataStreamServiceClient { get; }
    
    public OrdersStreamService.OrdersStreamServiceClient OrdersStreamServiceClient { get; }
}