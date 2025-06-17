using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Repositories;

public interface IProductRepository
{ 
    Task<Guid> CreateAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByNameAsync(string name);

    Task<bool> HasItemWithName(string name);
}