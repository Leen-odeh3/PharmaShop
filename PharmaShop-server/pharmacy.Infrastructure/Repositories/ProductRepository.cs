using Microsoft.EntityFrameworkCore;
using pharmacy.Core.DTOs.Product;
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
    public async Task<ProductResponseDto> CreateAsync(ProductRequestDto productRequest)
    {
        var discount = await _context.discounts
            .FirstOrDefaultAsync(d => d.DiscountId == productRequest.discountId);

        if (discount == null)
        {
            discount = new Discount { Percentage = 0 };
        }

        var product = new Product
        {
            ProductName = productRequest.ProductName,
            ProductDescription = productRequest.ProductDescription,
            Price = productRequest.Price,
            CategoryId = productRequest.CategoryId,
            BrandId = productRequest.BrandId,
            Discount = discount 
        };

        _context.products.Add(product);
        await _context.SaveChangesAsync();

        var productResponseDto = new ProductResponseDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Price = product.Price,
            Category = product.Category?.CategoryName, 
            Brand = product.Brand?.BrandName, 
            Percentage = product.Discount?.Percentage ?? 0 
        };

        return productResponseDto;
    }
}
