﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Implementation.EntityConfigurations
{
    public class CurrencyBucketTypeConfiguration : IEntityTypeConfiguration<CurrencyBucket>
    {
        public void Configure(EntityTypeBuilder<CurrencyBucket> builder)
        {
            builder.ToTable("CurrencyBuckets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("CurrencyBucketId");

            builder.Property(x => x.CurrencyCode)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.CurrencyName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasDefaultValue(0)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasMany(x => x.Transactions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(CurrencyBucket.Transactions))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}