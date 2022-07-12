using System;
using System.Threading;
using System.Threading.Tasks;
using NSA.Tinkoff.InvestApi.Services;
using NSA.Tinkoff.InvestApi.Testing;
using Tinkoff.InvestApi.V1;
using Xunit;
using OperationsService = NSA.Tinkoff.InvestApi.Services.OperationsService;

namespace NSA.Tinkoff.InvestApi.UnitTests;

public class OperationsServiceTests
{
    private readonly IOperationsService _operationsService;

    private readonly DateTimeOffset _from = new DateTimeOffset(2020, 01, 01, 0, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _to = new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero);
    
    public OperationsServiceTests()
    {
        var client = TinkoffApiClientProvider.GetInstance();
        _operationsService = new OperationsService(client);
    }

    [Fact]
    public async Task GetOperationsAsync_ReturnsNotNullResult()
    {   
        // Arrange
        var accountId = TinkoffApiTestingEnvAccessor.GetAccountId();
        
        // Act
        var operations = await _operationsService.GetOperationsAsync(null, _from, _to, OperationState.Executed, accountId, CancellationToken.None);

        // Assert
        Assert.NotNull(operations);
    }
    
    [Fact]
    public async Task GetPortfolioAsync_ReturnsPortfolio()
    {   
        // Arrange
        var accountId = TinkoffApiTestingEnvAccessor.GetAccountId();
        
        // Act
        var portfolio = await _operationsService.GetPortfolioAsync(accountId, CancellationToken.None);

        // Assert
        Assert.NotNull(portfolio);
    }
    
    [Fact]
    public async Task GetPositionsAsync_ReturnsPositions()
    {   
        // Arrange
        var accountId = TinkoffApiTestingEnvAccessor.GetAccountId();
        
        // Act
        var positions = await _operationsService.GetPositionsAsync(accountId, CancellationToken.None);

        // Assert
        Assert.NotNull(positions);
    }
}
