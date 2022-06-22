using NSA.Tinkoff.InvestApi.Contracts;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class UsersService : IUsersService
{
    private readonly ITinkoffApiClient _apiClient;

    private static readonly V1GetInfoRequest GetInfoRequest = new();
    
    public UsersService(ITinkoffApiClient apiClient)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
    }
    
    public Task<V1GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return _apiClient.UsersService_GetInfoAsync(GetInfoRequest, cancellationToken);
    }
}