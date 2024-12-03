using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class ReviewRepository:GenericRepository<Review> ,IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context):base(context)
    {
        
    }
}
