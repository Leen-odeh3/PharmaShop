
using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface ICartRepository
{
    Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts);
}
