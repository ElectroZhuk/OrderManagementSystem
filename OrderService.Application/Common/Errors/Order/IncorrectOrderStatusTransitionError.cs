namespace OrderService.Application.Common.Errors.Order;

public class IncorrectOrderStatusTransitionError : IBusinessError
{
    public string Code => "Order.Status.IncorrectTransition";
    public string Description => "Cant transit order from current status to provided.";
}