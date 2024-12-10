using pharmacy.Core.Entities.OrderAggregate;
namespace pharmacy.Core.Repositories.Contract;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
}
