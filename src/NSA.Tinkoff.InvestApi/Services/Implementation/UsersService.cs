using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Exceptions;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class UsersService : IUsersService
{
    private readonly IInvestApiClient _client;

    private static readonly GetInfoRequest GetInfoRequest = new();
    private static readonly GetAccountsRequest GetAccountsRequest = new();
    private static readonly GetUserTariffRequest GetUserTariffRequest = new();

    public UsersService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = _client.UsersServiceClient.GetInfoAsync(GetInfoRequest, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GetAccountsResponse> GetAccountsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = _client.UsersServiceClient.GetAccountsAsync(GetAccountsRequest, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GetUserTariffResponse> GetUserTariffAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result =
                _client.UsersServiceClient.GetUserTariffAsync(GetUserTariffRequest, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    /// <inheritdoc/>
    /// <remarks>If margin trading disabled on account, method will throw an exception.</remarks>
    public async Task<GetMarginAttributesResponse> GetMarginAttributesAsync(string accountId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetMarginAttributesRequest
            {
                AccountId = accountId
            };

            var result =
                _client.UsersServiceClient.GetMarginAttributesAsync(request, null, null,
                    cancellationToken);
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