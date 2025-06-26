namespace OrderService.Application.Common.Errors.Order;

public class ProductPriceLessThanOrEqualToZeroError : IBusinessError
{
    public string Code => "Order.Product.PriceLessThanOrEqualToZero";
    public string Description => "Product price can't be less than or equal to zero.";
}