using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;
using pharmacy.Infrastructure.Repositories;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.CheckOutAchieves.AddRangeAsync(checkouts);
            var result = await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
