using Mapster;
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

    public async Task<IEnumerable<ReviewResponseDto>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(c => c.Customer)
            .Include(p => p.Product)
            .ToListAsync();

        foreach (var review in reviews)
        {
            var customerEmail = review.Customer?.Email ?? "Not Available";
            var productName = review.Product?.ProductName ?? "Not Available";

            _log.Log($"Review ID {review.ReviewId} has customer email {customerEmail} and product name {productName}.", "info");
        }

        return reviews.Select(review => new ReviewResponseDto
        {
            ReviewId = review.ReviewId,
            Comment = review.Comment,
            Rating = review.Rating,
            CreatedDate = review.CreatedDate,
            Email = review.Customer?.Email ?? "Not Available", 
            ProductName = review.Product?.ProductName ?? "Not Available" 
        }).ToList();
    }
}
