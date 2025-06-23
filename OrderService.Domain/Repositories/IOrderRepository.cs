using OrderService.Domain.Entities;

namespace OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    
    Task CreateAsync(Order order);

    Task UpdateAsync(Order order);
}