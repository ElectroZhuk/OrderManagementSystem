namespace OrderService.Application.Common.Errors.Order;

public static class OrderErrors
{
    public static readonly ProductsAmountLessThanOneError ProductsAmountLessThanOneError = new();
    public static readonly ProductQuantityLessThanOneError ProductQuantityLessThanOneError = new();
    public static readonly ProductPriceLessThanOrEqualToZeroError ProductPriceLessThanOrEqualToZeroError = new();
    public static readonly OrderNotFoundByIdError OrderNotFoundByIdError = new();
    public static readonly CantTransitOrderInFinalStatusError CantTransitOrderInFinalStatusError = new();
    public static readonly CantTransitOrderToInitialStatusError CantTransitOrderToInitialStatusError = new();
    public static readonly IncorrectOrderStatusTransitionError IncorrectOrderStatusTransitionError = new();
}