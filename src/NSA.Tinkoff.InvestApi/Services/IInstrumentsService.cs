using Tinkoff.InvestApi.V1;

namespace NSA.Tinkoff.InvestApi.Services;

public interface IInstrumentsService
{
    /// <summary>
    /// Get instrument by Id.
    /// </summary>
    /// <param name="id">Ticker, Figi or Uuid of instrument.</param>
    /// <param name="idType">Type of id. <see cref="InstrumentIdType"/>.</param>
    /// <param name="classCode">Class of instrument.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<Instrument?> GetInstrumentById(string id,
        InstrumentIdType idType,
        string? classCode,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of bonds.
    /// </summary>
    /// <param name="instrumentStatus">Status of instrument. <see cref="InstrumentStatus"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IReadOnlyCollection<Bond>> GetBondsAsync(InstrumentStatus instrumentStatus, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of coupons of specified bond.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <param name="figi">Figi of a bond.</param>
    /// <param name="from">From date.</param>
    /// <param name="to">To date.</param>
    public Task<IReadOnlyCollection<Coupon>> GetBondCouponsAsync(string figi, DateTimeOffset from,
        DateTimeOffset to, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of currencies.
    /// </summary>
    /// <param name="instrumentStatus">Status of instrument. <see cref="InstrumentStatus"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IReadOnlyCollection<Currency>> GetCurrenciesAsync(InstrumentStatus instrumentStatus, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of shares.
    /// </summary>
    /// <param name="instrumentStatus">Status of instrument. <see cref="InstrumentStatus"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IReadOnlyCollection<Share>> GetSharesAsync(InstrumentStatus instrumentStatus, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get list of etfs.
    /// </summary>
    /// <param name="instrumentStatus">Status of instrument. <see cref="InstrumentStatus"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    Task<IReadOnlyCollection<Etf>> GetEtfsAsync(InstrumentStatus instrumentStatus, CancellationToken cancellationToken = default);
}
