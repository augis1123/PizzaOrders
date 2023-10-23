using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Dtos.Orders
{
    public record OrderDto(Guid Id, Guid PizzaId);
    public record CreateOrderDto(Guid PizzaId);
}
