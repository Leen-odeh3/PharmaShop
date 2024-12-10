using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class OrderRepository :GenericRepository<Order> ,IOrderRepository
{

    public OrderRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerEmail)
    {
        return await _context.Orders
                             .Include(o => o.Items)
                             .ThenInclude(oi => oi.Product)
                             .Where(o => o.BuyerEmail == customerEmail)
                             .ToListAsync();
    }
}
