using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data;

namespace CatalogService.Infrastructure.Repositories;

public class ProductRepository(ProductContext productContext)
{
    public async Task Create(Product product)
    {
        await productContext.Products.AddAsync(product);
    }
}