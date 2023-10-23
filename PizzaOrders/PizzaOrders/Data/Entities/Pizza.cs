using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Entities
{
    public class Pizza
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SizeId { get; internal set; }
        public double Price { get; set; }
    }
}
