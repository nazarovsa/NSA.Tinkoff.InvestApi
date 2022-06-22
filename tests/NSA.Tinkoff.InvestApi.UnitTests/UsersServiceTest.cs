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
}