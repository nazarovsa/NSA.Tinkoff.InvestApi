using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IOrdersService
{
    /// <summary>
    /// Get orders.
    /// </summary>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<GetOrdersResponse?> GetOrdersAsync(string? accountId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get state of an order.
    /// </summary>
    /// <param name="orderId">Id of an order.</param>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<OrderState?> GetOrderStateAsync(string orderId, string? accountId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Post order.
    /// </summary>
    /// <param name="figi">Figi of an instrument.</param>
    /// <param name="direction">Direction of an order.</param>
    /// <param name="orderType">Type of an order.</param>
    /// <param name="quantity">Quantity of an order.</param>
    /// <param name="price">Price of an order.</param>
    /// <param name="idempotenceId">Idempotence id. Max 36 symbols.</param>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<PostOrderResponse?> PostOrderAsync(
        string figi,
        OrderDirection direction,
        OrderType orderType,
        int quantity,
        Quotation price,
        string? idempotenceId,
        string? accountId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancel order.
    /// </summary>
    /// <param name="orderId">Id of an order.</param>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<CancelOrderResponse?> CancelOrderAsync(
        string orderId,
        string? accountId = null,
        CancellationToken cancellationToken = default);

}