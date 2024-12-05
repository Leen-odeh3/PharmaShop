using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class OrderRepository :GenericRepository<Order> ,IOrderRepository
{

    public OrderRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }
}
