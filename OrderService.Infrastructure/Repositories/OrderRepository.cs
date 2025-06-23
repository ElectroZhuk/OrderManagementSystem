using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository(OrderContext orderContext) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await orderContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task CreateAsync(Order order)
    {
        order.CreatedDateUtc = DateTime.UtcNow;

        await orderContext.Orders.AddAsync(order);
        await orderContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        order.UpdatedDateUtc = DateTime.UtcNow;

        orderContext.Orders.Update(order);
        await orderContext.SaveChangesAsync();
    }
}