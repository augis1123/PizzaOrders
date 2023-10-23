using PizzaOrders.Data;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.SeedData
{
    public class SeedData
    {
        public static async Task SeedSizesAsync(PizzaOrdersDbContext context)
        {
            if (context.Sizes.Count() == null || context.Sizes.Count() == 0)
            {
                var s1 = new Size { Name = "Small", Price = 8.0 };
                var s2 = new Size { Name = "Medium", Price = 10.0 };
                var s3 = new Size { Name = "Large", Price = 12.0 };
                await context.Sizes.AddAsync(s1);
                await context.Sizes.AddAsync(s2);
                await context.Sizes.AddAsync(s3);
                context.SaveChanges();
            }
        }

        public static async Task SeedToppingsAsync(PizzaOrdersDbContext context)
        {
            if (context.Toppings.Count() == null || context.Toppings.Count() == 0)
            {
                var t1 = new Topping { Name = "Pepperoni" };
                var t2 = new Topping { Name = "Mushrooms" };
                var t3 = new Topping { Name = "Onions" };
                await context.Toppings.AddAsync(t1);
                await context.Toppings.AddAsync(t2);
                await context.Toppings.AddAsync(t3);
                context.SaveChanges();
            }
        }
    }
}
