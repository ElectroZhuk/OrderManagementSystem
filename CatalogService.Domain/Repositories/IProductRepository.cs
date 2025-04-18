using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Repositories;

public interface IProductRepository
{ 
    Task CreateAsync(Product product);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByNameAsync(string name);

    Task<bool> HasItemWithName(string name);
}