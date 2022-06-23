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
}