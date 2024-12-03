using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class OrderItemRepository : GenericRepository<OrderItem> ,IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public Task<OrderItem> CreateAsync(OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderItem>> GetItemsByOrderIdAsync(int orderId)
    {
        return await _context.ordersItem
                             .Where(oi => oi.OrderId == orderId)
                             .Include(oi => oi.Product)  
                             .ToListAsync();
    }

}
