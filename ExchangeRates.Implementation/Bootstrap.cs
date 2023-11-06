using ExchangeRates.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRates.Implementation;

public class Bootstrap
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IExchangeRateService, ExchangeRateService>();
    }
}