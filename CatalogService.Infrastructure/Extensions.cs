using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ProductContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=catalogservice;Username=postgres;");
        });
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();

        return serviceCollection;
    }
}