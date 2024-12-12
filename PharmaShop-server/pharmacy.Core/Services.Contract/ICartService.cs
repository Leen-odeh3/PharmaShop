using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Services.Contract;
public interface ICartService
{
    Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
    Task<ServiceResponse> CheckOut(Checkout checkout);
}
