using CatalogService.Application.Dtos;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.Services;

internal class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task CreateAsync(CreateProductDto product)
    {
        var newProduct = new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedDateUtc = DateTime.UtcNow
        };
        
        if (await GetByNameAsync(newProduct.Name) is not null)
            throw new ArgumentException();

        await productRepository.CreateAsync(newProduct);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await productRepository.GetByIdAsync(id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await productRepository.GetByNameAsync(name);
    }
}