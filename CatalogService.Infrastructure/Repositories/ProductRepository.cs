using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;
using CatalogService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories;

internal class ProductRepository(ProductContext productContext) : IProductRepository
{
    public async Task CreateAsync(Product product)
    {
        product.CreatedDateUtc = DateTime.UtcNow;
        await productContext.Products.AddAsync(product);
        await productContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        product.UpdatedDateUtc = DateTime.UtcNow;
        productContext.Update(product);
        await productContext.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        return await productContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<bool> HasItemWithName(string name)
    {
        return await productContext.Products.AnyAsync(p => p.Name == name);
    }
}