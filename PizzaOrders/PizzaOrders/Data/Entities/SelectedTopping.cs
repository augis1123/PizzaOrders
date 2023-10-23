using System.ComponentModel.DataAnnotations;

namespace PizzaOrders.Data.Entities
{
    public class SelectedTopping
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PizzaId { get; set; }
        public Guid ToppingId { get; set; }
    }
}
