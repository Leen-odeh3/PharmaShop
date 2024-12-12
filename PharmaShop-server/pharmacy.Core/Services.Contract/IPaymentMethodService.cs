using pharmacy.Core.Entities;
using System.Collections.Generic;

namespace pharmacy.Core.Services.Contract;
public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethod();
}
