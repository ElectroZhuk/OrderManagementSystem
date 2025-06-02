using System.Net;

namespace CatalogService.Application.Exceptions;

public class ProductNotFoundByIdException(Guid id) 
    : BusinessLogicException($"Не найден продукт с Id='{id}'.", HttpStatusCode.NotFound);