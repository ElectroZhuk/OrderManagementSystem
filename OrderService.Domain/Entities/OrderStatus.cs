namespace OrderService.Domain.Entities;

public enum OrderStatus
{
    New,
    Assembling,
    Assembled,
    Delivery,
    Delivered,
    Cancelled
}