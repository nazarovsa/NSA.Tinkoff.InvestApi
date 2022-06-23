using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IUsersService
{
    /// <summary>
    /// Get user's info.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="GetInfoResponse"/>.</returns>
    Task<GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user's accounts.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="GetAccountsResponse"/>.</returns>
    Task<GetAccountsResponse> GetAccountsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user's tariff.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="GetUserTariffResponse"/>.</returns>
    Task<GetUserTariffResponse> GetUserTariffAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get margin attributes calculation.
    /// </summary>
    /// <param name="accountId">Account id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="GetMarginAttributesResponse"/>.</returns>
    Task<GetMarginAttributesResponse> GetMarginAttributesAsync(string accountId,
        CancellationToken cancellationToken = default);


}