using System.Net;

namespace CatalogService.Application.Exceptions;

public class NewProductQuantityLessThenZeroException(int newQuantity) :
    BusinessLogicException($"Некорректное новое количество продукта после уменьшения: {newQuantity}.", HttpStatusCode.UnprocessableEntity);