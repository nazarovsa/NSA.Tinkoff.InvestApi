using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IOperationsService
{
    /// <summary>
    /// Get operations.
    /// </summary>
    /// <param name="figi">Filter by figi.</param>
    /// <param name="from">Filter from date.</param>
    /// <param name="to">Filter to date.</param>
    /// <param name="operationState">State of an operation.</param>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<OperationsResponse?> GetOperationsAsync(
        string? figi,
        DateTimeOffset from,
        DateTimeOffset to,
        OperationState operationState,
        string accountId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get positions.
    /// </summary>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<PositionsResponse?> GetPositionsAsync(string accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get portfolio.
    /// </summary>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<PortfolioResponse?> GetPortfolioAsync(string accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get withdraw limits.
    /// </summary>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<WithdrawLimitsResponse?> GetWithdrawLimitsAsync(string accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate report with dividends of foreign issuers. 
    /// </summary>
    /// <param name="from">Date from.</param>
    /// <param name="to">Date to.</param>
    /// <param name="accountId">Id of an account.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task<GenerateDividendsForeignIssuerReportResponse?> GenerateDividendsForeignIssuerReportAsync(
        DateTimeOffset from,
        DateTimeOffset to,
        string accountId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get report with dividends of foreign issuers.
    /// </summary>
    /// <param name="taskId">Id of a report from <see cref="GenerateDividendsForeignIssuerReportAsync"/>.</param>
    /// <param name="page">Page number.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<GetDividendsForeignIssuerReportResponse?> Get(string taskId, int page = 0, CancellationToken cancellationToken = default);
}
