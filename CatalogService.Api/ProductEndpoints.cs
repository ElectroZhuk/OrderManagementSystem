using CatalogService.Api.Dtos;
using CatalogService.Api.Dtos.Validators;
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

        group.MapGet("/{id:Guid}", GetAsync);
        group.MapPost("", CreateAsync);
        group.MapPut("/{id:Guid}", UpdateAsync);
        group.MapPatch("/{id:Guid}/quantity", UpdateQuantityAsync);
        group.MapDelete("/{id:Guid}", DeleteAsync);
    }

    private static async Task<IResult> GetAsync(Guid id, IProductService productService)
    {
        return Results.Ok(await productService.GetAsync(id));
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
        var updatedProduct = new UpdateProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Price = product.Price,
            Quantity = product.Quantity
        };

        await productService.UpdateAsync(id, updatedProduct);

        return Results.NoContent();
    }
    
    private static async Task<IResult> UpdateQuantityAsync(Guid id, UpdateProductQuantityRequest newQuantity, IProductService productService)
    {
        await productService.UpdateQuantityAsync(id, newQuantity.DecreaseAmount);

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsync(Guid id, IProductService productService)
    {
        await productService.DeleteAsync(id);

        return Results.NoContent();
    }
}