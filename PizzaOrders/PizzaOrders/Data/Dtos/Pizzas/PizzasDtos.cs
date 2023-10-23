using PizzaOrders.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Dtos.Pizzas
{
    public record PizzaDto(Guid Id, Guid Size, double Price);
    public record SecondPizzaDto(string size, List<string> Toppings, double price);
    public record CreatePizzaDto(Guid Size, List<Guid> Toppings, double Price);
    public record DeletePizzaDto(Guid Id, Guid Size, double Price);
    public record CalculatePricePizzaDto(Guid Size, List<Guid>? Toppings);
}
