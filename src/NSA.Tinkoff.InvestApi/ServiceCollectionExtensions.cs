using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;

namespace NSA.Tinkoff.InvestApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTinkoffApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        
        var options = configuration
            .GetSection(nameof(InvestApiOptions))
            .Get<InvestApiOptions>();

        return services.AddTinkoffApiClient(options);
    } 
    
    
    public static IServiceCollection AddTinkoffApiClient(this IServiceCollection services, InvestApiOptions options)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(options.AccessToken))
        {
            throw new ArgumentException("AccessToken should be specified.", nameof(InvestApiOptions.AccessToken));
        }
        
        var applicationName = string.IsNullOrWhiteSpace(options.ApplicationName)
            ? "NSA.Tinkoff.InvestApi"
            : options.ApplicationName;
        
        services.AddHttpClient<ITinkoffApiClient, TinkoffApiClient>(client =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken);
            client.DefaultRequestHeaders.Add("x-app-name", applicationName);
        });
        
        return services;
    }
}