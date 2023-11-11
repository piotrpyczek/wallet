using Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Common;

public class ErrorDetails : IErrorDetails
{
    public ErrorDetails(Exception exception)
    {
        StatusCode = GetStatusCode(exception);
        Errors = GetErrors(exception);
        Detail = exception.Message;
    }

    public int StatusCode { get; set; }
    public string Detail { get; set; }
    public IReadOnlyDictionary<string, string[]> Errors { get; set; }

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.ErrorsDictionary;
        }

        return null;
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
}