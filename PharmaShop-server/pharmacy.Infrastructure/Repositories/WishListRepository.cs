using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class WishListRepository:GenericRepository<WishlistItem> ,IWishListRepositry
{
    public WishListRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<IReadOnlyList<Product>?> GetAllProductForUserByEmailAsync(string email)
    {
        var product = await _context.WishlistItems.Where(w => w.UserEmail == email)
                                                     .Include(w => w.Product)
                                                     .ThenInclude(p => p.Brand)
                                                     .Include(w => w.Product)
                                                     .ThenInclude(p => p.Category)
                                                     .Select(w => w.Product)
                                                     .ToListAsync();
        if (product is null)
            return null;

        return product;

    }

    public async Task<WishlistItem?> GetWishlistobjAsync(string email, int productid)
    {
        var result = await _context.WishlistItems.Where(w => w.UserEmail == email && w.ProductId == productid)
                                                   .FirstOrDefaultAsync();
        if (result is null)
            return null;
        return result;
    }
}

