using NSA.Tinkoff.InvestApi.Contracts;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IUsersService
{
    /// <summary>
    /// Get user's info.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns><see cref="V1GetInfoResponse"/>.</returns>
    Task<V1GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default);
}