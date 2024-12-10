using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Core.Services.Contract;
public interface IOrderService
{
    Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
    Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail);
    Task<Order?> GetOrderByIdforUserAsync(int orderId, string buyerEmail);
}


