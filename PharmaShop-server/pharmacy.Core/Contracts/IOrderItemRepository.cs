using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts;
public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
}