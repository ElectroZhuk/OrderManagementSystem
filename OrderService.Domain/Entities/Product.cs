namespace OrderService.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}