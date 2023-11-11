using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Wallet.Implementation;

namespace Wallet.API.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            AddDbContext(services, configuration);
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

        private static void AddModules(IServiceCollection services)
        {

        }
    }
}
