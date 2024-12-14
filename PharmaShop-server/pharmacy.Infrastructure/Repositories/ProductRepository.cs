using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
    private IQueryable<Product> GetProductWithIncludes()
    {
        return _context.products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Discount);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await GetProductWithIncludes().ToListAsync();
    }

    public async Task<Product> GetByID(int id)
    {
        return await GetProductWithIncludes().FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<Product> GetProductImagesAsync(int id)
    {
        return await _context.products
            .Where(p => p.ProductId == id)
            .Select(p => new Product
            {
                ProductId = p.ProductId,
                ImageUrls = p.ImageUrls
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Product> UpdateAsync(int id, Product entity)
    {
        var existingEntity = await _context.products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Discount)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();

        return existingEntity; 
    }
    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
    {
        return await GetProductWithIncludes()
                             .Where(p => EF.Functions.Like(p.ProductName, $"%{name}%"))
                             .ToListAsync();
    }
}
