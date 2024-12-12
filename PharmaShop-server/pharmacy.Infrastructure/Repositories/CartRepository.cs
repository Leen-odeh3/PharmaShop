using pharmacy.Core;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;
using System.Runtime.CompilerServices;

namespace pharmacy.Infrastructure.Repositories;
public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
    {
        await _context.CheckOutAchieves.AddRangeAsync(checkouts);
        return await _context.SaveChangesAsync();
    }
}
