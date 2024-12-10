using pharmacy.Core.DTOs.Review;

namespace pharmacy.Core.Services.Contract;
public interface IReviewService
{
    Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto reviewRequestDto);
    Task<ReviewResponseDto> UpdateReviewAsync(int id, ReviewRequestDto reviewRequestDto);
    Task<string> DeleteReviewAsync(int id);
    Task<ReviewResponseDto> GetReviewByIdAsync(int id);
    Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync();
}