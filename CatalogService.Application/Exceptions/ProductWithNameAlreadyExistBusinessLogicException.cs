using System.Net;

namespace CatalogService.Application.Exceptions;

public class ProductWithNameAlreadyExistException(string name)
    : BusinessLogicException($"Продукт с названием '{name}' уже существует.", HttpStatusCode.BadRequest);