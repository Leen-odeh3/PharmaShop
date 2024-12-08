using pharmacy.Core.Entities;
namespace pharmacy.Core.Contracts;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
}
