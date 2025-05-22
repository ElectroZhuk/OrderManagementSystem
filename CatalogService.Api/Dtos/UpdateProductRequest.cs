namespace CatalogService.Api.Dtos;

public class UpdateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}