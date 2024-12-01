using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class DiscountRepository : GenericRepository<Discount> , IDiscountRepository
{
    public DiscountRepository(ApplicationDbContext context):base(context)
    {
        
    }

    public async Task<bool> CheckDiscountIsActiveOrNot(int id)
    {
        var discount = await _context.discounts.FindAsync(id);

        if (discount != null)
        {
            var start = discount.StartDateUtc;
            var end = discount.EndDateUtc;

            if (start <= DateTime.UtcNow && end >= DateTime.UtcNow)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now)
    {
        return await _context.discounts
                             .Where(d => d.StartDateUtc <= now && d.EndDateUtc >= now) 
                             .OrderByDescending(d => d.Percentage) 
                             .Take(topN)
                             .ToListAsync();
    }

    public async Task<int> GetTotalPrice(int id)
    {
        var discount = await _context.discounts.FindAsync(id);

        if (discount == null)  return 0;

        var product = await _context.products
                                     .Where(p => p.discount.DiscountId == id)  
                                     .FirstOrDefaultAsync();

        if (product is null)
            return 0;

        var price = product.Price;
        var discountAmount = (price * discount.Percentage) / 100; 
        var totalPrice = price - discountAmount;

        return (int)totalPrice; 
    }

}
