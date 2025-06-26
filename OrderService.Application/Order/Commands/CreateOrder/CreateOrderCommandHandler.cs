
using ErrorOr;
using MediatR;
using OrderService.Application.Common.Errors.Order;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateProducts(request.Products);
        if (validationResult.IsError)
            return validationResult.Errors;
        
        var order = new Domain.Entities.Order
        {
            Products = request.Products.ToList(),
            Status = OrderStatusTransition.Initial,
            TotalPrice = request.Products.Sum(p => p.Price * p.Quantity)
        };
        
        return await orderRepository.CreateAsync(order, cancellationToken);
    }

    private static ErrorOr<Success> ValidateProducts(IReadOnlyCollection<Domain.Entities.Product> products)
    {
        if (products.Count < 1) 
            return Error.Validation(OrderErrors.ProductsAmountLessThanOneError.Code,
                OrderErrors.ProductsAmountLessThanOneError.Description);
        
        if (products.Any(p => p.Quantity < 1))
            return Error.Validation(OrderErrors.ProductQuantityLessThanOneError.Code,
                OrderErrors.ProductQuantityLessThanOneError.Description,
                new ()
                {
                    [nameof(products)] = products
                        .Where(p => p.Quantity < 1)
                        .Select(p => p.Id).ToArray()
                });
        
        if (products.Any(p => p.Price <= 0))
            return Error.Validation(OrderErrors.ProductPriceLessThanOrEqualToZeroError.Code,
                OrderErrors.ProductPriceLessThanOrEqualToZeroError.Description,
                new ()
                {
                    [nameof(products)] = products
                        .Where(p => p.Price <= 0)
                        .Select(p => p.Id).ToArray()
                });

        return Result.Success;
    }
}