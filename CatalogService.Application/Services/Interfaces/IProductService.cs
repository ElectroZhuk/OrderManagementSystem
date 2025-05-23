using CatalogService.Application.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services.Interfaces;

public interface IProductService
{
    Task CreateAsync(CreateProductDto product);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product?> GetByNameAsync(string name);
    
    
}