using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Roboline.Service.Exceptions;

namespace Roboline.WebApi.Middleware;

public sealed class DefaultGlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            NotFoundException nf => new ProblemDetails
            {
                Title = "NotFound",
                Detail = nf.Message,
                Status = StatusCodes.Status404NotFound
            },
            BadRequestException br => new ProblemDetails
            {
                Title = "BadRequest",
                Detail = br.Message,
                Status = StatusCodes.Status400BadRequest
            },
            _ => new ProblemDetails()
            {
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}