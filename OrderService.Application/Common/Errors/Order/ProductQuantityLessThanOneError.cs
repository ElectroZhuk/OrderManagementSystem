namespace OrderService.Application.Common.Errors.Order;

public sealed class ProductQuantityLessThanOneError : IBusinessError
{
    public string Code => "Order.Product.QuantityLessThanOne";
    public string Description => "Product quantity can't be less than one.";
}