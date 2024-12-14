using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IReviewRepository : IGenericRepository<Review>
{
   Task<IEnumerable<ReviewResponseDto>> GetReviewsByProductIdAsync(int productId);
    Task<IEnumerable<ReviewResponseDto>> GetReviewsWithProductAndCustomerAsync();
}