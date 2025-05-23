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
}