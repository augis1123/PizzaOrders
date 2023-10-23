using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Entities
{
    public class Size
    {
        [Key]
        public Guid Id {get; set;}

        [Required]
        public string Name { get; set;}

        [Required]
        public double Price { get; set; }

    }
}
