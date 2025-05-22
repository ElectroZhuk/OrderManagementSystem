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
            Quantity = product.Quantity
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

    public async Task UpdateAsync(Guid id, UpdateProductDto product)
    {
        var foundProduct = await GetByIdAsync(id);

        if (foundProduct is null)
        {
            var exception = new ArgumentException();
            logger.Error(exception, "Product с ID=\"{Id}\" не существует.", id);

            throw exception;
        }

        if (product.Name is not null)
            foundProduct.Name = product.Name;
        
        if (product.Description is not null)
            foundProduct.Description = product.Description;
        
        if (product.Category is not null)
            foundProduct.Category = product.Category;
        
        if (product.Price is not null)
            foundProduct.Price = product.Price.Value;
        
        if (product.Quantity is not null)
            foundProduct.Quantity = product.Quantity.Value;

        await productRepository.UpdateAsync(foundProduct);
        
        logger.Information("Обновлен продукт {@Product}.", foundProduct);
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