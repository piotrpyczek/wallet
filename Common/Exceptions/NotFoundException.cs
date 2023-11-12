namespace Common.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() : this("Not found") { }
    public NotFoundException(string message) : base(message) { }
}