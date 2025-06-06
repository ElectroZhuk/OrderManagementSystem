using CatalogService.Application.Common.Logging;
using CatalogService.Application.Dtos;
using CatalogService.Application.Exceptions;
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
            throw new ProductWithNameAlreadyExistException(newProduct.Name);
        }

        await productRepository.CreateAsync(newProduct);
        
        logger.Information("Создан продукт {@Product}.", newProduct);
    }

    public async Task UpdateAsync(Guid id, UpdateProductDto product)
    {
        if (id != product.Id)
            throw new UpdatedProductIdDoesNotMatchException(id, product.Id);
        
        var foundProduct = await GetByIdAsync(id);
        
        if (foundProduct is null)
            throw new ProductNotFoundByIdException(id);
        
        if (product.Name != foundProduct.Name && await productRepository.HasItemWithName(product.Name))
            throw new ProductWithNameAlreadyExistException(product.Name);
        
        foundProduct.Name = product.Name;
        foundProduct.Description = product.Description;
        foundProduct.Category = product.Category;
        foundProduct.Price = product.Price;
        foundProduct.Quantity = product.Quantity;

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