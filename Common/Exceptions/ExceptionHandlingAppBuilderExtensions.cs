
using Microsoft.AspNetCore.Builder;

namespace Common.Exceptions;

public static class ExceptionHandlingAppBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}