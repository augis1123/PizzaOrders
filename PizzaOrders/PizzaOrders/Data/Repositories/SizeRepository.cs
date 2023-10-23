using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;
using PizzaOrders.SeedData;
namespace PizzaOrders.Data.Repositories
{
    public interface ISizeRepository
    {
        Task<Size?> GetAsync(Guid SizeId);
        Task<IReadOnlyList<Size>> GetManyAsync();
    }
    public class SizeRepository : ISizeRepository
    {
        private readonly PizzaOrdersDbContext _context;

        public SizeRepository(PizzaOrdersDbContext context)
        {
            _context = context;
            SeedData.SeedData.SeedSizesAsync(_context);
        }

        public async Task<Size?> GetAsync(Guid SizeId)
        {
            return await _context.Sizes.FirstOrDefaultAsync(o => o.Id == SizeId);
        }

        public async Task<IReadOnlyList<Size>> GetManyAsync()
        {
            return await _context.Sizes.ToListAsync();
        }


    }
}
