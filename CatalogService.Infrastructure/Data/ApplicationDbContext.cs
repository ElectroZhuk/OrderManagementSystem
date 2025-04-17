using Microsoft.EntityFrameworkCore;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Data;
 
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
 
    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=");
    }
}