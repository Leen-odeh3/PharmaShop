using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Core.Repositories.Contract;
public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}