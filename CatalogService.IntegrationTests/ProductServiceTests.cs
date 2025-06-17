using CatalogService.Application.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.IntegrationTests;

public class ProductServiceTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private CreateProductDto InitCreateProductDto()
    {
        return new CreateProductDto()
        {
            Name = "Тестовое название",
            Category = "Тестовая категория",
            Description = "Тестовое описание",
            Price = 10m,
            Quantity = 0
        };
    }
    
    private UpdateProductDto InitUpdateProductDto(Guid id)
    {
        return new UpdateProductDto()
        {
            Id = id,
            Name = "Тестовое обновленное название",
            Category = "Тестовая обновленная категория",
            Description = "Тестовое обновленное описание",
            Price = 100m,
            Quantity = 10
        };
    }
    
    [Fact]
    public async Task CreateAsync_ShouldCreateProductInDatabase_WhenProductIsValid()
    {
        // Arrange
        var product = InitCreateProductDto();

        // Act
        var createdProductId = await ProductService.CreateAsync(product);

        // Assert
        var createdProduct = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == createdProductId);
        Assert.NotNull(createdProduct);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProductInDatabase_WhenProductIsValid()
    {
        // Arrange
        var existsProductId = await ProductService.CreateAsync(InitCreateProductDto());
        var updateProduct = InitUpdateProductDto(existsProductId);

        // Act
        await ProductService.UpdateAsync(existsProductId, updateProduct);

        // Assert
        var updatedProduct = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == existsProductId);
        Assert.NotNull(updatedProduct);
        Assert.Multiple(
            () => Assert.Equal(updateProduct.Name, updatedProduct.Name),
            () => Assert.Equal(updateProduct.Category, updatedProduct.Category),
            () => Assert.Equal(updateProduct.Description, updatedProduct.Description),
            () => Assert.Equal(updateProduct.Price, updatedProduct.Price),
            () => Assert.Equal(updateProduct.Quantity, updatedProduct.Quantity)
        );
    }

    [Fact]
    public async Task UpdateQuantityAsync_ShouldUpdateProductQuantityInDatabase_WhenQuantityIsValid()
    {
        // Arrange
        var existProduct = InitCreateProductDto();
        var existsProductId = await ProductService.CreateAsync(existProduct);
        var newQuantity = Random.Shared.Next();

        // Act
        await ProductService.UpdateQuantityAsync(existsProductId, newQuantity);

        // Assert
        var updatedProduct = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == existsProductId);
        Assert.NotNull(updatedProduct);
        Assert.Equal(newQuantity, updatedProduct.Quantity);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProductFromDatabase_WhenIdIsValid()
    {
        // Arrange
        var existProduct = InitCreateProductDto();
        var existsProductId = await ProductService.CreateAsync(existProduct);
        
        // Act
        var foundProduct = await ProductService.GetAsync(existsProductId);

        // Assert
        Assert.NotNull(foundProduct);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProductFromDatabase_WhenIdIsValid()
    {
        // Arrange
        var existProduct = InitCreateProductDto();
        var existsProductId = await ProductService.CreateAsync(existProduct);
        
        // Act
        await ProductService.DeleteAsync(existsProductId);

        // Assert
        var deletedProduct = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == existsProductId);
        Assert.Null(deletedProduct);
    }
};