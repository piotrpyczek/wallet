
using Common.Exceptions;
using ExchangeRates.Implementation.Configuration;

namespace ExchangeRates.API.Infrastructure;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        AddApplicationOptions(services, configuration);
        AddMiddlewares(services);
        AddModules(services);

        return services;
    }

    private static void AddApplicationOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeRatesDatabaseSettings>(configuration.GetSection(ExchangeRatesDatabaseSettings.Section));
    }

    private static void AddMiddlewares(IServiceCollection services)
    {
        services.AddSingleton<ExceptionHandlingMiddleware>();
    }

    private static void AddModules(IServiceCollection services)
    {
        Implementation.Bootstrap.Register(services);
    }
}