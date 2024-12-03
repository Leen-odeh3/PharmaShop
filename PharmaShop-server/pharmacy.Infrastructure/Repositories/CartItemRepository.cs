using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class CartItemRepository:GenericRepository<CartItem> , ICartItemRepository
{
    public CartItemRepository(ApplicationDbContext context):base(context)
    {
        
    }
}
