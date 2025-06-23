namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products;
    public OrderStatus Status { get; set; }
    public decimal TotalPrice => Products.Sum(p => p.Price);
    public DateTime CreatedDateUtc { get; set; }
    public DateTime UpdatedDateUtc { get; set; }

    public void AddProduct(Product product) => _products.Add(product);
    
    public void AddProduct(IEnumerable<Product> products) => _products.AddRange(products);

    public void RemoveProduct(Product product) => _products.Remove(product);
    
    public void RemoveProduct(IEnumerable<Product> products) => _products.RemoveAll(products.Contains);
}