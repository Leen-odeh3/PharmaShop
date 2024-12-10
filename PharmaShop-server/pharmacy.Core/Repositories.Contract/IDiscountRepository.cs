using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IDiscountRepository : IGenericRepository<Discount>
{
    Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now);
}
