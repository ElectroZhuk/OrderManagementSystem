using CatalogService.Application.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services.Interfaces;

public interface IProductService
{
    Task<Product> GetAsync(Guid id);
    
    Task<Guid> CreateAsync(CreateProductDto product);

    Task UpdateAsync(Guid id, UpdateProductDto product);

    Task UpdateQuantityAsync(Guid productId, int decreaseAmount);

    Task DeleteAsync(Guid id);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByNameAsync(string name);
}