using System.Net;

namespace CatalogService.Application.Exceptions;

public class BusinessLogicException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
};