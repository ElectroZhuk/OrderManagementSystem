namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public List<Product> Products { get; set; } = new();
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime UpdatedDateUtc { get; set; }
}