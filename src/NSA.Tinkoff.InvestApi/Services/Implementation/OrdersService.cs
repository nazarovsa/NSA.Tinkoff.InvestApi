using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Exceptions;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class OrdersService : IOrdersService
{
    private readonly IInvestApiClient _client;

    private static readonly GetOrdersRequest GetOrdersRequest = new();
    private static readonly GetOrderStateRequest GetOrdersStateRequest = new();

    public OrdersService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<GetOrdersResponse?> GetOrdersAsync(string? accountId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = string.IsNullOrWhiteSpace(accountId)
                ? GetOrdersRequest
                : new GetOrdersRequest() { AccountId = accountId };

            var result = _client.OrdersServiceClient.GetOrdersAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<PostOrderResponse?> PostOrderAsync(
        string figi,
        OrderDirection direction,
        OrderType orderType,
        int quantity,
        Quotation price,
        string? idempotenceId,
        string? accountId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(figi))
            {
                throw new ArgumentNullException(nameof(figi));
            }

            if (idempotenceId is { Length: > 36 })
            {
                throw new ArgumentException($"{nameof(idempotenceId)}'s length should be less than 37 symbols.");
            }

            var request = new PostOrderRequest()
            {
                Figi = figi,
                Direction = direction,
                OrderType = orderType,
                Quantity = quantity,
                Price = price,
            };

            if (!string.IsNullOrWhiteSpace(accountId))
            {
                request.AccountId = accountId;
            }

            if (!string.IsNullOrWhiteSpace(idempotenceId))
            {
                request.OrderId = idempotenceId;
            }

            var result = _client.OrdersServiceClient.PostOrderAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<CancelOrderResponse?> CancelOrderAsync(
        string orderId,
        string? accountId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new CancelOrderRequest()
            {
                OrderId = orderId,
            };

            if (!string.IsNullOrWhiteSpace(accountId))
            {
                request.AccountId = accountId;
            }

            var result = _client.OrdersServiceClient.CancelOrderAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<OrderState?> GetOrderStateAsync(string orderId, string? accountId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(orderId))
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            var request = new GetOrderStateRequest { OrderId = orderId, AccountId = accountId };

            var result = _client.OrdersServiceClient.GetOrderStateAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }
}