using ErrorOr;
using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Order.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(Guid Id, OrderStatus NewStatus) : IRequest<ErrorOr<Updated>>;