using CatalogService.Api.Dtos;
using CatalogService.Application.Dtos;
using CatalogService.Application.Services.Interfaces;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace CatalogService.Api;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api/products")
            .WithOpenApi()
            .AddFluentValidationAutoValidation();

        group.MapPost("", CreateAsync);
        group.MapPut("/{id:Guid}", UpdateAsync);
    }

    private static async Task<IResult> CreateAsync(CreateProductRequest product, IProductService productService)
    {
        var newProduct = new CreateProductDto()
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Quantity = product.Quantity
        };
        
        await productService.CreateAsync(newProduct);

        return Results.NoContent();
    }

    private static async Task<IResult> UpdateAsync(Guid id, UpdateProductRequest product, IProductService productService)
    {
        var updatedProduct = new UpdateProductDto();
        
        if (product.Name is not null)
            updatedProduct.Name = product.Name;
        
        if (product.Description is not null)
            updatedProduct.Description = product.Description;
        
        if (product.Category is not null)
            updatedProduct.Category = product.Category;
        
        if (product.Price is not null)
            updatedProduct.Price = product.Price.Value;
        
        if (product.Quantity is not null)
            updatedProduct.Quantity = product.Quantity.Value;

        await productService.UpdateAsync(id, updatedProduct);

        return Results.NoContent();
    }
}