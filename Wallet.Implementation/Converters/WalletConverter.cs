using Wallet.Implementation.DataObjects;

namespace Wallet.Implementation.Converters;

public static class WalletConverter
{
    public static WalletDTO ToWalletDTO(this Domain.Entities.Wallet wallet)
    {
        return new WalletDTO
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Currencies = wallet.Buckets.Select(x => x.ToCurrencyBucketDTO())
        };
    }
}