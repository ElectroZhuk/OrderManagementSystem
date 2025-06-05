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
    public static IServiceCollection AddDockerDataAccess(this IServiceCollection serviceCollection, IConfigurationManager configuration)
    {
        return serviceCollection.AddDataAccess(configuration, "CatalogServiceDocker");
    }
    
    public static IServiceCollection AddDefaultDataAccess(this IServiceCollection serviceCollection, IConfigurationManager configuration)
    {
        return serviceCollection.AddDataAccess(configuration, "CatalogService");
    }

    public static IServiceCollection AddLogger(this IServiceCollection serviceCollection, IConfigurationManager configuration)
    {
        var configuredLogger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        
        return serviceCollection.AddSingleton<IAppLogger>(new SerilogLogger(configuredLogger));
    }

    public static void ApplyMigrations(this IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
    
        context.Database.CanConnect();
        context.Database.Migrate();
        
        scope.Dispose();
    }
    
    private static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection, IConfigurationManager configuration, string connectionStringPath)
    {
        serviceCollection.AddDbContext<ProductContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(connectionStringPath));
        });
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();

        return serviceCollection;
    }
}