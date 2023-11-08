using Wallet.Domain.Infrastructure;

namespace Wallet.Domain.Entities;

public class Wallet : Entity
{
    public string Name { get; set; }
    public ICollection<CurrencyBucket> Buckets { get; set; }
}