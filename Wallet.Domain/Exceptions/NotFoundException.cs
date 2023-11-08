namespace Wallet.Domain.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() : base("Not found") { }
}