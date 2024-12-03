using pharmacy.Core;
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
}
