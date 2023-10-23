using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Repositories
{
    public interface ISelectedToppingRepository
    {
        Task<SelectedTopping?> GetAsync(Guid SelectedToppingId);
        Task<IReadOnlyList<SelectedTopping>> GetManyByPizzaAsync(Guid pizzaId);
        Task CreateAsync(SelectedTopping selectedTopping);
    }
    public class SelectedToppingRepository : ISelectedToppingRepository
    {
        private readonly PizzaOrdersDbContext _context;

        public SelectedToppingRepository(PizzaOrdersDbContext context)
        {
            _context = context;
        }

        public async Task<SelectedTopping?> GetAsync(Guid SelectedToppingId)
        {
            return await _context.SelectedToppings.FirstOrDefaultAsync(o => o.Id == SelectedToppingId);
        }

        public async Task<IReadOnlyList<SelectedTopping>> GetManyByPizzaAsync(Guid pizzaId)
        {
            return await _context.SelectedToppings.Where(o => o.PizzaId == pizzaId).ToListAsync();
        }
        public async Task CreateAsync(SelectedTopping selectedTopping)
        {
            _context.SelectedToppings.Add(selectedTopping);
            await _context.SaveChangesAsync();
        }
    }
}