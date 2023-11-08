namespace Wallet.Domain.Infrastructure;

public interface IEntity<TId>
{
    TId? Id { get; set; }
}