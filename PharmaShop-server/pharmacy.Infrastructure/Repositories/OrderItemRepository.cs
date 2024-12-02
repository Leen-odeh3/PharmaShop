using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class OrderItemRepository : GenericRepository<OrderItemRepository> ,IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(int orderId)
    {
        return await _context.ordersItem
                             .Where(oi => oi.OrderID == orderId)
                             .Include(oi => oi.Product)  
                             .ToListAsync();
    }
}
