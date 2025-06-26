namespace OrderService.Application.Common.Errors.Order;

public class OrderNotFoundByIdError : IBusinessError
{
    public string Code => "Order.NotFoundById";
    public string Description => "Order not found by provided Id.";
}