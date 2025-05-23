namespace CatalogService.Api.Dtos;

public class CreateProductRequest
{
    public string Name { get; set; } // Уникальное
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}