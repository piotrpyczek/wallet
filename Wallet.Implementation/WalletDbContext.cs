using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Implementation.EntityConfigurations;

namespace Wallet.Implementation
{
    public class WalletDbContext : DbContext
    {
        public DbSet<Domain.Entities.Wallet> Wallets { get; set; }
        public DbSet<CurrencyBucket> CurrencyBuckets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WalletTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyBucketTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
        }
    }
}
