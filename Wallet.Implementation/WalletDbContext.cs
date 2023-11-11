using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        #region Transaction

        private IDbContextTransaction? currentTransaction;

        public bool HasActiveTransaction => currentTransaction != null;

        public async Task<IDbContextTransaction?> BeginTransactionAsync()
        {
            if (currentTransaction != null)
            {
                return null;
            }

            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        #endregion
    }
}

