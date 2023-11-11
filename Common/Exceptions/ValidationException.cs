namespace Common.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : base()
    {
        ErrorsDictionary = errorsDictionary;
    }

    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}