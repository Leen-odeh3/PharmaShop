using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class DiscountRepository : GenericRepository<Discount> , IDiscountRepository
{
    public DiscountRepository(ApplicationDbContext context):base(context)
    {
        
    }

    public async Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now)
    {
        return await _context.discounts
                             .Where(d => d.StartDateUtc <= DateTime.Now && d.EndDateUtc >= DateTime.Now ) 
                             .OrderByDescending(d => d.Percentage) 
                             .Take(topN)
                             .ToListAsync();
    }

}
