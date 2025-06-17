using CatalogService.Application.Common.Logging;
using CatalogService.Application.Dtos;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using Moq;

namespace CatalogService.UnitTests;

public class ProductServiceTests
{
    private readonly ProductService _productService;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    
    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new(_productRepositoryMock.Object, Mock.Of<IAppLogger>());
    }
    
    private static Product CreateTestProduct()
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = "Тестовый продукт",
            Description = "Тестовое описание",
            Category = "Тестовая категория",
            Price = 10m,
            Quantity = 0,
            CreatedDateUtc = DateTime.UtcNow
        };
    }

    private void SetupRepositoryGetById(Guid id, Product? returnValue)
    {
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(returnValue);
    }

    private void SetupRepositoryHasItemWithName(string name, bool returnValue)
    {
        _productRepositoryMock.Setup(repo => repo.HasItemWithName(name)).ReturnsAsync(returnValue);
    }

    private static CreateProductDto CreateProductDto(string name = "Тестовый продукт") =>
        new()
        {
            Name = name,
            Description = "Тестовое описание",
            Category = "Тестовая категория",
            Price = 10m,
            Quantity = 0
        };

    private static UpdateProductDto CreateUpdateProductDto(Guid? id = null, string name = "Тестовое имя") =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            Name = name,
            Description = "Тестовое описание",
            Category = "Тестовая категория",
            Price = 10m,
            Quantity = 0
        };
    
    [Fact]
    public async Task GetAsync_Should_ThrowProductNotFoundByIdException_WhenGettingNonExistentProduct()
    {
        // Arrange
        var nonExistentProductId = Guid.NewGuid();
        SetupRepositoryGetById(nonExistentProductId, null);
        
        // Act
        var exception = await Assert.ThrowsAsync<ProductNotFoundByIdException>(() => 
            _productService.GetAsync(nonExistentProductId));
        
        // Assert
        Assert.Contains(nonExistentProductId.ToString(), exception.Message);
    }

    [Fact]
    public async Task GetAsync_Should_ReturnProduct_WhenProductFound()
    {
        // Arrange
        var existentProduct = CreateTestProduct();
        SetupRepositoryGetById(existentProduct.Id, existentProduct);
        
        // Act
        var foundProduct = await _productService.GetAsync(existentProduct.Id);
        
        // Assert
        Assert.Equal(existentProduct, foundProduct);
    }
    
    [Fact]
    public async Task CreateAsync_Should_ThrowProductWithNameAlreadyExistException_WhenCreatingProductWitheExistentName()
    {
        // Arrange
        var createProductDto = CreateProductDto();
        SetupRepositoryHasItemWithName(createProductDto.Name, true);
        
        // Act
        var exception = await Assert.ThrowsAsync<ProductWithNameAlreadyExistException>(() =>
            _productService.CreateAsync(createProductDto));
        
        // Assert
        Assert.Contains(createProductDto.Name, exception.Message);
    }
    
    [Fact]
    public async Task CreateAsync_Should_CallCreateAsyncOnRepository_WhenProductCreated()
    {
        // Arrange
        var createProductDto = CreateProductDto("Уникальный продукт");
        SetupRepositoryHasItemWithName(createProductDto.Name, false);
        
        // Act
        await _productService.CreateAsync(createProductDto);
        
        // Assert
        _productRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Product>(p => 
                p.Name == createProductDto.Name &&
                p.Description == createProductDto.Description &&
                p.Category == createProductDto.Category &&
                p.Price == createProductDto.Price &&
                p.Quantity == createProductDto.Quantity)), 
            Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_Should_ThrowUpdatedProductIdDoesNotMatchException_WhenProductIdsMismatch()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productWithDifferentId = CreateUpdateProductDto();
        
        // Act
        var exception = await Assert.ThrowsAsync<UpdatedProductIdDoesNotMatchException>(() =>
            _productService.UpdateAsync(productId, productWithDifferentId));
        
        // Assert
        Assert.Contains(productId.ToString(), exception.Message);
        Assert.Contains(productWithDifferentId.Id.ToString(), exception.Message);
    }
    
    [Fact]
    public async Task UpdateAsync_Should_ThrowProductWithNameAlreadyExistException_WhenUpdatingProductNameToExisting()
    {
        // Arrange
        var updateProductDto = CreateUpdateProductDto(name: "Новое тестовое имя");
        var foundProduct = CreateTestProduct();
        foundProduct.Id = updateProductDto.Id;
        
        SetupRepositoryGetById(updateProductDto.Id, foundProduct);
        SetupRepositoryHasItemWithName(updateProductDto.Name, true);
        
        // Act
        var exception = await Assert.ThrowsAsync<ProductWithNameAlreadyExistException>(() =>
            _productService.UpdateAsync(updateProductDto.Id, updateProductDto));
        
        // Assert
        Assert.Contains(updateProductDto.Name, exception.Message);
    }
    
    [Fact]
    public async Task UpdateAsync_Should_CallUpdateAsyncOnRepository_WhenProductUpdate()
    {
        // Arrange
        var updateProductDto = CreateUpdateProductDto(name: "Новое тестовое имя");
        var foundProduct = CreateTestProduct();
        foundProduct.Id = updateProductDto.Id;
        
        SetupRepositoryGetById(updateProductDto.Id, foundProduct);
        SetupRepositoryHasItemWithName(updateProductDto.Name, false);
        
        // Act
        await _productService.UpdateAsync(updateProductDto.Id, updateProductDto); 
        
        // Assert
        _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Product>(p =>
                p.Id == updateProductDto.Id &&
                p.Name == updateProductDto.Name &&
                p.Description == updateProductDto.Description &&
                p.Category == updateProductDto.Category &&
                p.Price == updateProductDto.Price &&
                p.Quantity == updateProductDto.Quantity &&
                p.CreatedDateUtc == foundProduct.CreatedDateUtc)),
            Times.Once);
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    [InlineData(-100)]
    [InlineData(int.MinValue)]
    public async Task UpdateQuantityAsync_Should_ThrowIncorrectNewProductQuantity_WhenNewQuantityLessThenZero(
        int negativeQuantity)
    {
        // Act
        var exception = await Assert.ThrowsAsync<IncorrectNewProductQuantityException>(() =>
            _productService.UpdateQuantityAsync(Guid.NewGuid(), negativeQuantity));
        
        // Assert
        Assert.Contains(negativeQuantity.ToString(), exception.Message);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    [InlineData(int.MaxValue)]
    public async Task UpdateQuantityAsync_Should_CallUpdateAsyncOnRepository_WhenQuantityUpdated(int newQuantity)
    {
        // Arrange
        var product = CreateTestProduct();
        SetupRepositoryGetById(product.Id, product);
        
        // Act
        await _productService.UpdateQuantityAsync(product.Id, newQuantity);
        
        // Assert
        _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Product>(p =>
                p.Id == product.Id &&
                p.Name == product.Name &&
                p.Description == product.Description &&
                p.Category == product.Category &&
                p.Price == product.Price &&
                p.Quantity == newQuantity &&
                p.CreatedDateUtc == product.CreatedDateUtc)),
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_Should_CallDeleteAsyncOnRepository_WhenProductDeleted()
    {
        //Arrange
        var deletedProduct = CreateTestProduct();
        SetupRepositoryGetById(deletedProduct.Id, deletedProduct);
        
        // Act
        await _productService.DeleteAsync(deletedProduct.Id);
        
        // Assert
        _productRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Product>(p =>
                p.Id == deletedProduct.Id &&
                p.Name == deletedProduct.Name &&
                p.Description == deletedProduct.Description &&
                p.Category == deletedProduct.Category &&
                p.Price == deletedProduct.Price &&
                p.Quantity == deletedProduct.Quantity &&
                p.CreatedDateUtc == deletedProduct.CreatedDateUtc)),
            Times.Once);
    }
}