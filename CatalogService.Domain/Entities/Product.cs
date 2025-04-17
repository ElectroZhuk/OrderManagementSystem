namespace CatalogService.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } // Уникальное
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime UpdatedDateUtc { get; set; }
}