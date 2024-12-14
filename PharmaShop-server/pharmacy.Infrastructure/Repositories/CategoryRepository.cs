using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category> ,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        return await _context.categories
            .Where(c => c.CategoryId == categoryId)
            .Include(c => c.Products)
            .ThenInclude(p => p.Discount) 
            .SelectMany(c => c.Products) 
            .ToListAsync();
    }

}
