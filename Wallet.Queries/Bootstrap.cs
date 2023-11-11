using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Wallet.Queries;

public static class Bootstrap
{
    public static void Register(IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(currentAssembly));

        services.AddValidatorsFromAssembly(currentAssembly);
    }
}