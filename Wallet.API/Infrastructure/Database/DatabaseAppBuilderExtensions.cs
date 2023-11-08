using Microsoft.EntityFrameworkCore;
using Wallet.Implementation;

namespace Wallet.API.Infrastructure.Database
{
    public static class DatabaseAppBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
            context.Database.Migrate();

            return app;
        }
    }
}
