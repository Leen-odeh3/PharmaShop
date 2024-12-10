using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IDiscountRepository : IGenericRepository<Discount>
{
    Task<bool> CheckDiscountIsActiveOrNot(int id);
    Task<int> GetTotalPrice(int id);
    Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now);
}
