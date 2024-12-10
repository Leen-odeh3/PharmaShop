using Microsoft.EntityFrameworkCore;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class ReviewRepository:GenericRepository<Review> ,IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<IEnumerable<ReviewResponseDto>> GetAllAsync()
    {
        return (IEnumerable<ReviewResponseDto>)await _context.Reviews
            .Include(c => c.Customer)
            .Include(p => p.Product)
            .ToListAsync();
    }

}
