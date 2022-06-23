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
    public async Task Should_get_userInfo()
    {
        // Act
        var response = await _userService.GetInfoAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task Should_get_accounts()
    {
        // Act
        var response = await _userService.GetAccountsAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Accounts);
    }
    
    [Fact]
    public async Task Should_get_tariff()
    {
        // Act
        var response = await _userService.GetUserTariffAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
    
    [Fact(Skip = "Need account with enabled margin trading.")]
    public async Task Should_get_marginAttributes()
    {
        var accountId = "INSERT_ACCOUNT_ID_HERE";
        
        // Act
        var response = await _userService.GetMarginAttributesAsync(accountId, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
}