using Wallet.Domain.Entities;
using Wallet.Implementation.DataObjects;

namespace Wallet.Implementation.Converters;

public static class CurrencyBucketConverter
{
    public static CurrencyBucketDTO ToCurrencyBucketDTO(this CurrencyBucket currencyBucket)
    {
        return new CurrencyBucketDTO
        {
            Code = currencyBucket.CurrencyCode,
            Currency = currencyBucket.CurrencyName,
            Amount = currencyBucket.Amount
        };
    }
}