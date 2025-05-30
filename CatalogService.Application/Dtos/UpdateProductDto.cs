namespace CatalogService.Application.Dtos;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}