using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IPaymentMethodRepository
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
}
