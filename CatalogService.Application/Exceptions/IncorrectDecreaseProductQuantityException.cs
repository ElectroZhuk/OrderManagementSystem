using System.Net;

namespace CatalogService.Application.Exceptions;

public class IncorrectDecreaseProductQuantityException(int providedQuantity) 
    : BusinessLogicException($"Указано некорректное число для уменьшения количества продуктов: '{providedQuantity}'.", HttpStatusCode.UnprocessableEntity);