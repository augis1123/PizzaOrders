using Microsoft.EntityFrameworkCore;
using PizzaOrders.Data.Entities;

namespace PizzaOrders.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetAsync(Guid OrderId);
        Task<IReadOnlyList<Order>> GetManyAsync();
        Task CreateAsync(Order Order);
    }
    public class OrderRepository : IOrderRepository
    {
        private readonly PizzaOrdersDbContext _context;

        public OrderRepository(PizzaOrdersDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetAsync(Guid OrderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
        }

        public async Task<IReadOnlyList<Order>> GetManyAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task CreateAsync(Order Order)
        {
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
        }
    }
}