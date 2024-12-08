using pharmacy.Core.Contracts;
using pharmacy.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

using pharmacy.Core.Entities;

namespace pharmacy.Infrastructure.Repositories;
public class CartRepository :GenericRepository<Cart>,ICartRepository
{
    public CartRepository(ApplicationDbContext context):base(context)
    {
       
    }
    public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
    {
        return await _context.Carts
                             .Include(c => c.CartItems)
                             .ThenInclude(ci => ci.Product)
                             .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }
}
