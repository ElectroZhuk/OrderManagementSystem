using CatalogService.Application.Common.Logging;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Common.Logging;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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

    public static IServiceCollection AddLogger(this IServiceCollection serviceCollection, IConfigurationManager configuration)
    {
        var configuredLogger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        
        return serviceCollection.AddSingleton<IAppLogger>(new SerilogLogger(configuredLogger));
    }
}