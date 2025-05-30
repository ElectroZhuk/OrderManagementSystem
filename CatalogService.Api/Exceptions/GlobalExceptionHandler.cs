using CatalogService.Application.Common.Logging;
using CatalogService.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Exceptions;

public class GlobalExceptionHandler(IAppLogger logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.Error(exception, "Произошла ошибка {ExceptionType}.", exception.GetType().FullName!);

        var problemDetails = new ProblemDetails();
        int statusCode;

        if (exception is BusinessLogicException businessLogicException)
        {
            statusCode = (int)businessLogicException.StatusCode;
            problemDetails.Title = businessLogicException.Message;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            problemDetails.Title = "Произошла ошибка сервера.";
        }

        problemDetails.Status = statusCode;
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}