using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Exceptions;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public class UsersService : IUsersService
{
    private readonly IInvestApiClient _client;
    
    private static readonly GetInfoRequest GetInfoRequest = new();
    private static readonly GetUserTariffRequest GetUserTariffRequest = new();
    private static readonly GetAccountsRequest GetAccountsRequest = new();
    private static readonly GetMarginAttributesRequest GetMarginAttributesRequest = new();

    public UsersService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = _client.UsersServiceClient.GetInfoAsync(GetInfoRequest, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }
}