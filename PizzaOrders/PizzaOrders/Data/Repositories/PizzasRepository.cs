using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Repositories
{
    public interface IPizzassRepository
    {
        Task<Pizza?> GetAsync(Guid PizzaId);
        Task<IReadOnlyList<Pizza>> GetManyAsync();
        Task CreateAsync(Pizza pizza);
        Task DeleteAsync(Pizza pizza);
    }
    public class PizzasRepository : IPizzassRepository
    {
        private readonly PizzaOrdersDbContext _context;
        public PizzasRepository(PizzaOrdersDbContext pizzaOrdersDbContext) 
        {
            _context = pizzaOrdersDbContext;
        }

        public async Task<Pizza?> GetAsync(Guid PizzaId)
        {
            return await _context.Pizzas.FirstOrDefaultAsync(o => o.Id == PizzaId);
        }

        public async Task<IReadOnlyList<Pizza>> GetManyAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task CreateAsync(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pizza pizza)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
        }
    }
}
