namespace OrderService.Application.Common.Errors.Order;

public sealed class ProductsAmountLessThanOneError : IBusinessError
{
    public string Code => "Order.Products.AmountLessThanOne";
    public string Description => "Products amount can't be less than one.";
}