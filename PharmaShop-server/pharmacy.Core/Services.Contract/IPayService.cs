using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Services.Contract;
public interface IPayService
{
    Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<Cart> carts);
}
