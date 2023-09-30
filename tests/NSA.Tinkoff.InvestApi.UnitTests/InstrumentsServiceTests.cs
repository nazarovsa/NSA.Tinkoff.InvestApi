using System;
using System.Linq;
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
    public async Task GetInstrumentById_Figi_ReturnsInstrument()
    {
        // Act
        var instrument = await _instrumentsService.GetInstrumentById("BBG000BSJK37", InstrumentIdType.Figi, null, CancellationToken.None);

        // Assert
        Assert.NotNull(instrument);
        Assert.Equal("T", instrument!.Ticker);
    }

    [Fact]
    public async Task GetInstrumentById_Ticker_ReturnsInstrument()
    {
        // Act
        var instrument = await _instrumentsService.GetInstrumentById("T", InstrumentIdType.Ticker, "SPBXM", CancellationToken.None);

        // Assert
        Assert.NotNull(instrument);
        Assert.Equal("T", instrument!.Ticker);
    }

    [Fact]
    public async Task GetBondsAsync_ReturnsBonds()
    {
        // Act
        var bonds = await _instrumentsService.GetBondsAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(bonds);
        Assert.NotEmpty(bonds);
    }

    [Fact]
    public async Task GetSharesAsync_ReturnsShares()
    {
        // Act
        var shares = await _instrumentsService.GetSharesAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(shares);
        Assert.NotEmpty(shares);
    }

    [Fact]
    public async Task GetEtfsAsync_ReturnsEtfs()
    {
        // Act
        var etfs = await _instrumentsService.GetEtfsAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(etfs);
        Assert.NotEmpty(etfs);
    }

    [Fact]
    public async Task GetCurrenciesAsync_ReturnsCurrencies()
    {
        // Act
        var currencies = await _instrumentsService.GetCurrenciesAsync(InstrumentStatus.All, CancellationToken.None);

        // Assert
        Assert.NotNull(currencies);
        Assert.NotEmpty(currencies);
    }
}
