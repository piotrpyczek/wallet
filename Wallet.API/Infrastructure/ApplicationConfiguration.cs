using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wallet.API.Infrastructure.PipelineBehaviors;
using Wallet.Implementation;

namespace Wallet.API.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            AddDbContext(services, configuration);
            AddPipelineBehaviors(services);
            AddModules(services);

            return services;
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder options)
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
            }

            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WalletDB"), ConfigureSqlOptions);
            });
        }

        private static void AddPipelineBehaviors(IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void AddModules(IServiceCollection services)
        {
            Queries.Bootstrap.Register(services);
            Commands.Bootstrap.Register(services);
        }
    }
}
