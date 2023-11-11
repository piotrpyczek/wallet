namespace Common.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() : base("Not found") { }
}