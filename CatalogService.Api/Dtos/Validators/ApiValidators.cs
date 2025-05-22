using FluentValidation;

namespace CatalogService.Api.Dtos.Validators;

public static class ApiValidators
{
    public static IServiceCollection MapApiValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
        serviceCollection.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();

        return serviceCollection;
    }
}