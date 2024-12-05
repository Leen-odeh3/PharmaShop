
using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts;
public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
{
    Task<PaymentMethod> GetPaymentMethodByOrderIdAsync(int orderId);

}
