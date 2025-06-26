using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository(OrderContext orderContext) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await orderContext.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        order.CreatedDateUtc = DateTime.UtcNow;

        var createdOrder = await orderContext.Orders.AddAsync(order, cancellationToken);
        await orderContext.SaveChangesAsync(cancellationToken);

        return createdOrder.Entity.Id;
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        order.UpdatedDateUtc = DateTime.UtcNow;

        orderContext.Orders.Update(order);
        await orderContext.SaveChangesAsync(cancellationToken);
    }
}