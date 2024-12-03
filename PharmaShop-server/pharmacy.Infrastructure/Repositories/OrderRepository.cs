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
    public async Task<Order> GetOrderWithDetailsAsync(int orderId)
    {
        return await _context.orders
                             .Include(o => o.OrderItems)
                             .ThenInclude(oi => oi.Product)  
                             .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<decimal> CalculateOrderTotalAmount(int orderId)
    {
        var order = await GetOrderWithDetailsAsync(orderId);
        if (order is null) return 0;

        return order.OrderItems.Sum(item => item.Quantity * item.Price);
    }
}
