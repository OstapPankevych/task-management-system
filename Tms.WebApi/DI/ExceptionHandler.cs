using Microsoft.AspNetCore.Diagnostics;
using Tms.Common.Exceptions;

namespace Tms.WebApi.DI;

public class ExceptionHandler(ILogger<ExceptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        
        var exceptionResult = exception switch
        {
            ConflictException => (StatusCodes.Status409Conflict, LogLevel.Warning),
            NotFoundException => (StatusCodes.Status404NotFound, LogLevel.Information),
            ValidationException => (StatusCodes.Status400BadRequest, LogLevel.Information),
            _ => (StatusCodes.Status500InternalServerError, LogLevel.Error)
        };

        var exceptionMessage = exception.Message;
        logger.Log(exceptionResult.Item2, exception, "{message}", exceptionMessage);
            
        httpContext.Response.StatusCode = exceptionResult.Item1;
        var msg = httpContext.Response.StatusCode < StatusCodes.Status500InternalServerError ? exceptionMessage : "InternalServerError";
        await httpContext.Response.WriteAsync(msg, cancellationToken: cancellationToken);

        return true;
    }
}