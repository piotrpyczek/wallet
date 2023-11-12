using Wallet.Implementation;

namespace Wallet.API.Infrastructure.Database
{
    public static class DatabaseAppBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
            var logger = scope.ServiceProvider.GetService<ILogger<WalletDbContextMigration>>();

            new WalletDbContextMigration().Migrate(context, logger);

            return app;
        }
    }
}
