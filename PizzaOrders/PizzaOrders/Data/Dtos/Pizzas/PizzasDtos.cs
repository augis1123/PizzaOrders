using PizzaOrders.Data.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Dtos.Pizzas
{
    public record PizzaDto(Guid Id, PizzaSizeEnum Size, double Price);
    public record CreatePizzaDto(PizzaSizeEnum Size, double Price);
    public record DeletePizzaDto(Guid Id, PizzaSizeEnum Size, double Price);
    public record CalculatePricePizzaDto(PizzaSizeEnum Size, List<ToppingEnum>? Toppings);
}
