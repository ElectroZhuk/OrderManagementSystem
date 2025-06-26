namespace OrderService.Application.Common.Errors.Order;

public class CantTransitOrderInFinalStatusError : IBusinessError
{
    public string Code => "Order.Status.CantTransitInFinalStatus";
    public string Description => "Cant transit order in final status.";
}