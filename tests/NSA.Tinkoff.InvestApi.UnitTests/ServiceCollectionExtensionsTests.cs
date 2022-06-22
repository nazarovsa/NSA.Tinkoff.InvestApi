using Microsoft.Extensions.DependencyInjection;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;
using NSA.Tinkoff.InvestApi.Testing;
using Xunit;

namespace NSA.Tinkoff.InvestApi.UnitTests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void Should_add_apiClient_to_services()
    {
        // Arrange
        var services = new ServiceCollection();
        
        var options = new InvestApiOptions
        {
            AccessToken = AccessTokenAccessor.GetFromEnv(),
        };
        
        services.AddTinkoffApiClient(options);
        var sp = services.BuildServiceProvider();
        
        // Act
        var apiClient = sp.GetService<ITinkoffApiClient>();

        // Assert
        Assert.NotNull(apiClient);
    }
}