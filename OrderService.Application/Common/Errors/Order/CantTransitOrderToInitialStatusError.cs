namespace OrderService.Application.Common.Errors.Order;

public class CantTransitOrderToInitialStatusError : IBusinessError
{
    public string Code => "Order.Status.CantTransitToInitialStatus";
    public string Description => "Cant transit order to initial status.";
}