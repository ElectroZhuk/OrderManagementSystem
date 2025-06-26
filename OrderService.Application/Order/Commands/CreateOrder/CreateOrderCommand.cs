using ErrorOr;
using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Order.Commands.CreateOrder;

public record CreateOrderCommand(IReadOnlyCollection<Product> Products) : IRequest<ErrorOr<Guid>>;