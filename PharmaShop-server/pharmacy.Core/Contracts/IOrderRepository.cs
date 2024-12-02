using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order> GetOrderWithDetailsAsync(int orderId); 
    Task<decimal> CalculateOrderTotalAmount(int orderId); 
}
