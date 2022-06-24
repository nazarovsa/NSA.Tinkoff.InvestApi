using System.Threading;
using System.Threading.Tasks;
using NSA.Tinkoff.InvestApi.Services;
using NSA.Tinkoff.InvestApi.Testing;
using Tinkoff.InvestApi.V1;
using Xunit;
using InstrumentsService = NSA.Tinkoff.InvestApi.Services.InstrumentsService;

namespace NSA.Tinkoff.InvestApi.UnitTests;

public class InstrumentsServiceTests
{
    private readonly IInstrumentsService _instrumentsService;

    public InstrumentsServiceTests()
    {
        var client = TinkoffApiClientProvider.GetInstance();
        _instrumentsService = new InstrumentsService(client);
    }

    [Fact]
    public async Task Should_get_instrumentByFigi()
    {
        // Act
        var instrument = await _instrumentsService.GetInstrumentById("BBG000BSJK37", InstrumentIdType.Figi, null, CancellationToken.None);

        // Assert
        Assert.NotNull(instrument);
        Assert.Equal("T", instrument!.Ticker);
    }

    [Fact]
    public async Task Should_get_instrumentByTicker()
    {
        // Act
        var instrument = await _instrumentsService.GetInstrumentById("T", InstrumentIdType.Ticker, "SPBXM", CancellationToken.None);

        // Assert
        Assert.NotNull(instrument);
        Assert.Equal("T", instrument!.Ticker);
    }

    [Fact]
    public async Task Should_get_bonds()
    {
        // Act
        var bonds = await _instrumentsService.GetBondsAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(bonds);
        Assert.NotEmpty(bonds);
    }

    [Fact]
    public async Task Should_get_shares()
    {
        // Act
        var shares = await _instrumentsService.GetSharesAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(shares);
        Assert.NotEmpty(shares);
    }

    [Fact]
    public async Task Should_get_etfs()
    {
        // Act
        var etfs = await _instrumentsService.GetEtfsAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(etfs);
        Assert.NotEmpty(etfs);
    }

    [Fact]
    public async Task Should_get_currencies()
    {
        // Act
        var currencies = await _instrumentsService.GetCurrenciesAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(currencies);
        Assert.NotEmpty(currencies);
    }
}
