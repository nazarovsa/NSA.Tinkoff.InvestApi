using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Core.Utils;
using NSA.Tinkoff.InvestApi.Contracts;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class OrdersStreamService : IOrdersStreamService
{
    private readonly IInvestApiClient _client;

    public OrdersStreamService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
}