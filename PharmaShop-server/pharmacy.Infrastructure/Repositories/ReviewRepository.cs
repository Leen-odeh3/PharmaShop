using Microsoft.EntityFrameworkCore;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;
using pharmacy.Core.ILogger;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class ReviewRepository:GenericRepository<Review> ,IReviewRepository
{
    private readonly ILog _log;
    public ReviewRepository(ApplicationDbContext context,ILog log):base(context)
    {
        _log = log;
    }
    public async Task<IEnumerable<ReviewResponseDto>> GetReviewsWithProductAndCustomerAsync()
    {
        return await _context.Reviews
            .Include(r => r.Product) 
            .Include(r => r.Customer) 
            .Select(r => new ReviewResponseDto
            {
                ReviewId = r.ReviewId,
                Comment = r.Comment,
                Rating = r.Rating,
                CreatedDate = r.CreatedDate,
                ProductName = r.Product.ProductName,  
                Email = r.Customer.Email    
            })
            .ToListAsync();
    }
}
