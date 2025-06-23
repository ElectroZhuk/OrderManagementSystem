using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Logging;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Logging;
using OrderService.Infrastructure.Repositories;
using Serilog;

namespace OrderService.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<OrderContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("OrderService"));
        });
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();

        return serviceCollection;
    }
    
    public static IServiceCollection AddLogger(this IServiceCollection serviceCollection, IConfigurationManager configuration)
    {
        var configuredLogger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        
        return serviceCollection.AddSingleton<IAppLogger>(new SerilogLogger(configuredLogger));
    }
}