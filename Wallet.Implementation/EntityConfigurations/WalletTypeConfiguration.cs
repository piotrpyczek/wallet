using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Implementation.EntityConfigurations;

public class WalletTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Wallet>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Wallet> builder)
    {
        builder.ToTable("Wallets");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(x=>x.Buckets)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}