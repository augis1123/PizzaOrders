using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Repositories
{
    public interface IToppingRepository
    {
        Task<Topping?> GetAsync(Guid ToppingId);
        Task<IReadOnlyList<Topping>> GetManyAsync();
    }
    public class ToppingRepository : IToppingRepository
    {
        private readonly PizzaOrdersDbContext _context;

        public ToppingRepository(PizzaOrdersDbContext context)
        {
            _context = context;
            SeedData.SeedData.SeedToppingsAsync(_context);
        }

        public async Task<Topping?> GetAsync(Guid ToppingId)
        {
            return await _context.Toppings.FirstOrDefaultAsync(o => o.Id == ToppingId);
        }

        public async Task<IReadOnlyList<Topping>> GetManyAsync()
        {
            return await _context.Toppings.ToListAsync();
        }

    }
}
