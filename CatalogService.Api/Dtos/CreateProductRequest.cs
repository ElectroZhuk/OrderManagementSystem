namespace CatalogService.Api.Dtos;

public class CreateProductRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}