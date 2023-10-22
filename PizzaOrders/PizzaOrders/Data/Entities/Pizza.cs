using PizzaOrders.Data.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Entities
{
    public class Pizza
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public PizzaSizeEnum Size { get; set; }

        public double Price { get; set; }
    }
}
