using System.ComponentModel.DataAnnotations;
namespace PizzaOrders.Data.Entities
{
    public class Topping
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
