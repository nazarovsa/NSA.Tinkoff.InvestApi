using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSA.Tinkoff.InvestApi.Contracts;
using NSA.Tinkoff.InvestApi.Options;
using NSA.Tinkoff.InvestApi.Services;

namespace NSA.Tinkoff.InvestApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInvestApiClient(this IServiceCollection services,
        string name,
        IConfiguration configuration,
        Action<GrpcChannelOptions>? grpcChannelConfigurator = null)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var options = configuration
            .GetSection(nameof(InvestApiOptions))
            .Get<InvestApiOptions>();

        return services.AddInvestApiClient(name, options, grpcChannelConfigurator);
    }

    public static IServiceCollection AddInvestApiClient(this IServiceCollection services,
        string name,
        InvestApiOptions investApiOptions,
        Action<GrpcChannelOptions>? grpcChannelConfigurator = null)
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

        services.AddGrpcClient<InvestApiClient>(
                name,
                o => o.Address = new Uri("https://invest-public-api.tinkoff.ru:443"))
            .ConfigureChannel((_, options) =>
            {
                ConfigureGrpcChannel(options, investApiOptions);
                grpcChannelConfigurator?.Invoke(options);
            });

        services.AddSingleton<IInvestApiClient, InvestApiClient>(ctx => ctx.GetRequiredService<InvestApiClient>());

        return services;
    }

    /// <summary>
    /// Register <see cref="UsersService"/> as <see cref="IUsersService"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><param name="services"/> is null.</exception>
    public static IServiceCollection AddUsersService(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IUsersService, UsersService>();

        return services;
    }
    
    /// <summary>
    /// Register <see cref="OperationsService"/> as <see cref="IOperationsService"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><param name="services"/> is null.</exception>
    public static IServiceCollection AddOperationsService(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IOperationsService, OperationsService>();

        return services;
    }
    
    /// <summary>
    /// Register <see cref="InstrumentsService"/> as <see cref="IInstrumentsService"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><param name="services"/> is null.</exception>
    public static IServiceCollection AddInstrumentsService(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IInstrumentsService, InstrumentsService>();

        return services;
    }
    
    /// <summary>
    /// Register <see cref="UsersService"/> as <see cref="IUsersService"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><param name="services"/> is null.</exception>
    public static IServiceCollection AddOrdersService(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IOrdersService, OrdersService>();

        return services;
    }

    /// <summary>
    /// Register <see cref="MarketDataStreamService"/> as <see cref="IMarketDataStreamService"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><param name="services"/> is null.</exception>
    public static IServiceCollection AddMarketDataStreamService(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IMarketDataStreamService, MarketDataStreamService>();

        return services;
    }

    /// <summary>
    /// Method that configures GrpcChannelOptions by default.
    /// </summary>
    /// <param name="options"><see cref="GrpcChannelOptions"/>.</param>
    /// <param name="investApiOptions"><see cref="InvestApiOptions"/>.</param>
    /// <exception cref="ArgumentException"><see cref="InvestApiOptions.AccessToken"/> is empty.</exception>
    private static void ConfigureGrpcChannel(GrpcChannelOptions options, InvestApiOptions investApiOptions)
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
    }
}