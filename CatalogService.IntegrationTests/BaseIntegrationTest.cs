using CatalogService.Application.Services.Interfaces;
using CatalogService.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly IProductService ProductService;
    protected readonly ProductContext DbContext;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        ProductService = _scope.ServiceProvider.GetRequiredService<IProductService>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ProductContext>();
    }

    public void Dispose()
    {
        DbContext.Products.RemoveRange(DbContext.Products);
        DbContext.SaveChanges();
    }
}