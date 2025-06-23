namespace OrderService.Domain.Entities;

public enum OrderStatus
{
    New,
    Cancelled,
    Assembling,
    Assembled,
    Delivery,
    Delivered
}