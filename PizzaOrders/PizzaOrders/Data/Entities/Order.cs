using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PizzaId { get; set; }
    }
}
