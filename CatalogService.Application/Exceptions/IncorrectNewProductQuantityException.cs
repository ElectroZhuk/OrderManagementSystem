using System.Net;

namespace CatalogService.Application.Exceptions;

public class IncorrectNewProductQuantityException(int providedQuantity) 
    : BusinessLogicException($"Указано некорректное новое количество продукта: '{providedQuantity}'.", HttpStatusCode.BadRequest);