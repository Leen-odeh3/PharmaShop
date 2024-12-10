using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class OrderItemRepository : GenericRepository<OrderItem> ,IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext context):base(context)
    {
        
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.Id == orderId)
            .Include(oi => oi.Product)
            .ToListAsync();
    }

}
