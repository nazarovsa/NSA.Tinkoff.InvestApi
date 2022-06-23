using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Contracts;

public interface IInvestApiClient
{
    UsersService.UsersServiceClient UsersServiceClient { get; }

    MarketDataStreamService.MarketDataStreamServiceClient MarketDataStreamServiceClient { get; }
    
    InstrumentsService.InstrumentsServiceClient InstrumentsServiceClient { get; }
}