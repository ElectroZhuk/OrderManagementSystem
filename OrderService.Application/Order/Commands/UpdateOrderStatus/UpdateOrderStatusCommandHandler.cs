using ErrorOr;
using MediatR;
using OrderService.Application.Common.Errors.Extensions;
using OrderService.Application.Common.Errors.Order;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Order.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository) 
    : IRequestHandler<UpdateOrderStatusCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        if (OrderStatusTransition.Initial == request.NewStatus)
            return OrderErrors.CantTransitOrderToInitialStatusError.AsConflict(
                new ()
                {
                    [nameof(request.NewStatus)] = request.NewStatus    
                });
        
        var foundOrder = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (foundOrder is null)
            return OrderErrors.OrderNotFoundByIdError.AsNotFound(
                new ()
                {
                    [nameof(request.Id)] = request.Id
                });

        if (OrderStatusTransition.Final.Contains(foundOrder.Status))
            return OrderErrors.CantTransitOrderInFinalStatusError.AsConflict(
                new()
                {
                    [nameof(foundOrder.Status)] = foundOrder.Status
                });

        if (!OrderStatusTransition.IsAllowedTransition(foundOrder.Status, request.NewStatus))
            return OrderErrors.IncorrectOrderStatusTransitionError.AsConflict(
                new ()
                {
                    ["CurrentStatus"] = foundOrder.Status,
                    [nameof(request.NewStatus)] = request.NewStatus
                });

        foundOrder.Status = request.NewStatus;
        await orderRepository.UpdateAsync(foundOrder, cancellationToken);

        return Result.Updated;
    }
}