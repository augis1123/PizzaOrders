using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Dtos.Pizzas
{
    public record SelectedToppingDto(Guid Topping);
    public record CreateSelectedToppingDto(Guid PizzaId, Guid Topping);
}
