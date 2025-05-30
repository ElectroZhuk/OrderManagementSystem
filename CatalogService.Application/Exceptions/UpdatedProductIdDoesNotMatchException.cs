using System.Net;

namespace CatalogService.Application.Exceptions;

public class UpdatedProductIdDoesNotMatchException(Guid idInValue, Guid idInObject) 
    : BusinessLogicException($"Переданный Id='{idInValue}' не совпадает со значением в объекте Id='{idInObject}'.", HttpStatusCode.BadRequest)
{
    
}