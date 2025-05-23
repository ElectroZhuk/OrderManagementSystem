using CatalogService.Application.Common.Logging;
using CatalogService.Application.Dtos;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;

namespace CatalogService.Application.Services;

internal class ProductService(IProductRepository productRepository, IAppLogger logger) : IProductService
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

        if (await productRepository.HasItemWithName(newProduct.Name))
        {
            var exception = new ArgumentException();
            logger.Error(exception, "Product с именем {Name} уже существует.", newProduct.Name);
            
            throw exception;
        }

        await productRepository.CreateAsync(newProduct);
        
        logger.Information("Создан продукт {@Product}.", newProduct);
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