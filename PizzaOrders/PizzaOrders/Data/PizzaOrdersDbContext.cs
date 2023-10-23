using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data
{
    public class PizzaOrdersDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<SelectedTopping> SelectedToppings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase("PizzaOrdersDb");
            
        }
    }
}
