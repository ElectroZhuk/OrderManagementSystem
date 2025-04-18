using CatalogService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductService, ProductService>();
        
        return serviceCollection;
    }
}