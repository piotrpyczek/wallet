namespace Wallet.Domain.Infrastructure;

public abstract class Entity : IEntity<Guid?>
{
    public Guid? Id { get; set; }
}