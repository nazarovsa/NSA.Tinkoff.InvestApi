using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;
using NSA.Tinkoff.InvestApi.Services;
using UsersService = Tinkoff.InvestApi.V1.UsersService;

namespace NSA.Tinkoff.InvestApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInvestApiClient(this IServiceCollection services,
        string name,
        IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var options = configuration.GetSection(nameof(InvestApiOptions))
            .Get<InvestApiOptions>();

        return services.AddInvestApiClient(name, options);
    }

    public static IServiceCollection AddInvestApiClient(this IServiceCollection services,
        string name,
        InvestApiOptions investApiOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (investApiOptions == null)
        {
            throw new ArgumentNullException(nameof(investApiOptions));
        }

        services.AddGrpcClient<InvestApiClient>(name,
                o => o.Address = new Uri("https://invest-public-api.tinkoff.ru:443"))
            .ConfigureChannel((_, options) =>
            {
                if (string.IsNullOrWhiteSpace(investApiOptions.AccessToken))
                {
                    throw new ArgumentException("AccessToken should be specified.",
                        nameof(InvestApiOptions.AccessToken));
                }

                var applicationName = string.IsNullOrWhiteSpace(investApiOptions.ApplicationName)
                    ? "NSA.Tinkoff.InvestApi"
                    : investApiOptions.ApplicationName;

                var credentials = CallCredentials.FromInterceptor((_, metadata) =>
                {
                    metadata.Add("Authorization", $"Bearer {investApiOptions.AccessToken}");
                    metadata.Add("x-app-name", applicationName);

                    return Task.CompletedTask;
                });

                options.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);

                var defaultMethodConfig = new MethodConfig
                {
                    Names = { MethodName.Default },
                    RetryPolicy = new RetryPolicy
                    {
                        MaxAttempts = 5,
                        InitialBackoff = TimeSpan.FromSeconds(1),
                        MaxBackoff = TimeSpan.FromSeconds(5),
                        BackoffMultiplier = 1.5,
                        RetryableStatusCodes = { StatusCode.Unavailable }
                    }
                };

                options.ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } };
            });

        services.AddScoped<IInvestApiClient, InvestApiClient>(ctx => ctx.GetRequiredService<InvestApiClient>());

        return services;
    }
}