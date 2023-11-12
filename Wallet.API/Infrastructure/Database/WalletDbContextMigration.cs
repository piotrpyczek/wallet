using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Wallet.Implementation;

namespace Wallet.API.Infrastructure.Database
{
    public class WalletDbContextMigration
    {
        public void Migrate(WalletDbContext context, ILogger<WalletDbContextMigration> logger)
        {
            var policy = CreatePolicy(logger, nameof(WalletDbContextMigration));
            policy.Execute(() =>
            {
                context.Database.Migrate();
            });
        }

        private RetryPolicy CreatePolicy(ILogger<WalletDbContextMigration> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetry(
                    retries,
                    retry => TimeSpan.FromSeconds(5),
                    (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Error migrating database (attempt {retry} of {retries})", prefix, retry, retries);
                    }
                );
        }
    }
}
