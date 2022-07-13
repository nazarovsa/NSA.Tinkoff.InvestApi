using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Exceptions;
using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public sealed class OperationsService : IOperationsService
{
    private readonly IInvestApiClient _client;

    public OperationsService(IInvestApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<OperationsResponse?> GetOperationsAsync(string? figi,
        DateTimeOffset from,
        DateTimeOffset to,
        OperationState operationState,
        string accountId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new OperationsRequest
            {
                From = Timestamp.FromDateTimeOffset(from),
                To = Timestamp.FromDateTimeOffset(to),
                State = operationState,
                AccountId = accountId
            };

            if (!string.IsNullOrWhiteSpace(figi))
            {
                request.Figi = figi;
            }

            var result = _client.OperationsServiceClient.GetOperationsAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<PositionsResponse?> GetPositionsAsync(string accountId, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var request = new PositionsRequest
            {
                AccountId = accountId
            };

            var result = _client.OperationsServiceClient.GetPositionsAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<PortfolioResponse?> GetPortfolioAsync(string accountId, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var request = new PortfolioRequest
            {
                AccountId = accountId
            };

            var result = _client.OperationsServiceClient.GetPortfolioAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<WithdrawLimitsResponse?> GetWithdrawLimitsAsync(string accountId, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var request = new WithdrawLimitsRequest
            {
                AccountId = accountId
            };

            var result = _client.OperationsServiceClient.GetWithdrawLimitsAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            return await result.ResponseAsync;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GenerateBrokerReportResponse?> GenerateBrokerReportAsync(
        DateTimeOffset from,
        DateTimeOffset to,
        string accountId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var request = new BrokerReportRequest()
            {
                GenerateBrokerReportRequest = new GenerateBrokerReportRequest()
                {
                    From = Timestamp.FromDateTimeOffset(from),
                    To = Timestamp.FromDateTimeOffset(to),
                    AccountId = accountId
                }
            };

            var result = _client.OperationsServiceClient.GetBrokerReportAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            var response = await result.ResponseAsync;
            return response.GenerateBrokerReportResponse;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GetBrokerReportResponse?> GetBrokerReportAsync(
        string taskId,
        int page = 0,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new BrokerReportRequest()
            {
                GetBrokerReportRequest = new GetBrokerReportRequest
                {
                    TaskId = taskId,
                    Page = page
                }
            };

            var result = _client.OperationsServiceClient.GetBrokerReportAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            var response = await result.ResponseAsync;
            return response.GetBrokerReportResponse;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GenerateDividendsForeignIssuerReportResponse?> GenerateDividendsForeignIssuerReportAsync(
        DateTimeOffset from,
        DateTimeOffset to,
        string accountId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var request = new GetDividendsForeignIssuerRequest
            {
                GenerateDivForeignIssuerReport = new GenerateDividendsForeignIssuerReportRequest
                {
                    From = Timestamp.FromDateTimeOffset(from),
                    To = Timestamp.FromDateTimeOffset(to),
                    AccountId = accountId
                }
            };

            var result = _client.OperationsServiceClient.GetDividendsForeignIssuerAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            var response = await result.ResponseAsync;
            return response.GenerateDivForeignIssuerReportResponse;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }

    public async Task<GetDividendsForeignIssuerReportResponse?> GetDividendsForeignIssuerReportAsync(string taskId, int page = 0, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new ArgumentNullException(nameof(taskId));
            }

            var request = new GetDividendsForeignIssuerRequest
            {
                GetDivForeignIssuerReport = new GetDividendsForeignIssuerReportRequest
                {
                    TaskId = taskId,
                    Page = page
                }
            };

            var result = _client.OperationsServiceClient.GetDividendsForeignIssuerAsync(request, null, null, cancellationToken);
            if (result == null)
                throw new InvalidOperationException("Response is null.");

            var response = await result.ResponseAsync;
            return response.DivForeignIssuerReport;
        }
        catch (RpcException ex)
        {
            throw new ApiException(ex);
        }
    }
}
