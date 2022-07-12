using System.Threading;
using System.Threading.Tasks;
using NSA.Tinkoff.InvestApi.Services;
using NSA.Tinkoff.InvestApi.Testing;
using Xunit;

namespace NSA.Tinkoff.InvestApi.UnitTests;

public class UsersServiceTest
{
    private readonly IUsersService _userService;

    public UsersServiceTest()
    {
        var client = TinkoffApiClientProvider.GetInstance();
        _userService = new UsersService(client);
    }
    
    [Fact]
    public async Task GetInfoAsync_ReturnsUserInfo()
    {
        // Act
        var response = await _userService.GetInfoAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task GetAccountsAsync_ReturnsAccounts()
    {
        // Act
        var response = await _userService.GetAccountsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Accounts);
    }
    
    [Fact]
    public async Task GetUserTariffAsync_ReturnsTariff()
    {
        // Act
        var response = await _userService.GetUserTariffAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
    
    [Fact(Skip = "Requires account with enabled margin trading.")]
    public async Task GetMarginAttributesAsync_ReturnsAttributes()
    {
        var accountId = "INSERT_ACCOUNT_ID_HERE";
        
        // Act
        var response = await _userService.GetMarginAttributesAsync(accountId, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
}