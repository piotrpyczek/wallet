
using ExchangeRates.Implementation.Configuration;

namespace ExchangeRates.API.Infrastructure;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        AddApplicationOptions(services, configuration);
        AddModules(services);

        return services;
    }

    private static void AddApplicationOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExchangeRatesDatabaseSettings>(configuration.GetSection(ExchangeRatesDatabaseSettings.Section));
    }

    private static void AddModules(IServiceCollection services)
    {
        Implementation.Bootstrap.Register(services);
    }
}