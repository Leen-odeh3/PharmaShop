
using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class ProductRepository :GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.products
            .Include(p => p.Brand) 
            .Include(p => p.Category) 
            .ToListAsync(); 
    }

    public async Task<Product> GetByID(int id)
    {
        return await _context.products
            .Include(p => p.Brand) 
            .Include(p => p.Category) 
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }
}
