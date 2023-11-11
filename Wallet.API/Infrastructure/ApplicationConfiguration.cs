using Common.Exceptions;
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
            AddMiddlewares(services);
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

        private static void AddMiddlewares(IServiceCollection services)
        {
            services.AddSingleton<ExceptionHandlingMiddleware>();
        }

        private static void AddPipelineBehaviors(IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        }

        private static void AddModules(IServiceCollection services)
        {
            Queries.Bootstrap.Register(services);
            Commands.Bootstrap.Register(services);
        }
    }
}
