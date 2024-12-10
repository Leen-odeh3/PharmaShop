using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Repositories.Contract;

namespace pharmacy.Core.Contracts;
public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
}