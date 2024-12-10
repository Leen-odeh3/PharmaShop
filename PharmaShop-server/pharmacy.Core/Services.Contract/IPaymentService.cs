
using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Core.Services.Contract;
public interface IPaymentService
{
    Task<Order?> ChangeOrderStatuseAsync(string paymentintent, bool Flag);
    Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketid);
}