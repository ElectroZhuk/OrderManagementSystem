using OrderService.Domain.Entities;

namespace OrderService.Application.Order;

public static class OrderStatusTransition
{
    public static OrderStatus Initial => AllowedTransitions.Keys
        .Single(k => !AllowedTransitions.Values.Any(v => v.Contains(k)));

    public static IReadOnlyCollection<OrderStatus> Final => AllowedTransitions.Keys
        .Where(k => AllowedTransitions.GetValueOrDefault(k) is { Count: 0 }).ToArray();
    
    private static readonly Dictionary<OrderStatus, HashSet<OrderStatus>> AllowedTransitions = new()
    {
        [OrderStatus.New] = [OrderStatus.Assembling, OrderStatus.Cancelled],
        [OrderStatus.Assembling] = [OrderStatus.Assembled, OrderStatus.Cancelled],
        [OrderStatus.Assembled] = [OrderStatus.Delivery, OrderStatus.Cancelled],
        [OrderStatus.Delivery] = [OrderStatus.Delivered, OrderStatus.Cancelled],
        [OrderStatus.Delivered] = [],
        [OrderStatus.Cancelled] = []
    };

    public static bool IsAllowedTransition(OrderStatus from, OrderStatus to)
    {
        return GetAllowedTransitions(from).Contains(to);
    }
    
    public static IReadOnlyCollection<OrderStatus> GetAllowedTransitions(OrderStatus status)
    {
        return AllowedTransitions.GetValueOrDefault(status, []);
    }
}