using pharmacy.Core;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;
using Microsoft.Extensions.Logging;
using Mapster; 

namespace pharmacy.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(IUnitOfWork unitOfWork, ILogger<ReviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto reviewRequestDto)
    {
        try
        {
            _logger.LogInformation("Starting to add a new review.");
            var review = reviewRequestDto.Adapt<Review>();

            var createdReview = await _unitOfWork.reviewRepository.CreateAsync(review);
            _unitOfWork.Complete();

            var reviewsWithDetails = await _unitOfWork.reviewRepository.GetReviewsWithProductAndCustomerAsync();

            var reviewWithDetails = reviewsWithDetails.FirstOrDefault(r => r.ReviewId == createdReview.ReviewId);

            var reviewResponseDto = reviewWithDetails.Adapt<ReviewResponseDto>();

            _logger.LogInformation("Review added successfully with ID {ReviewId}.", createdReview.ReviewId);
            return reviewResponseDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding the review.");
            throw;
        }
    }

    public async Task<ReviewResponseDto> UpdateReviewAsync(int id, ReviewRequestDto reviewRequestDto)
    {
        try
        {
            _logger.LogInformation("Starting to update review with ID {ReviewId}.", id);

            var review = reviewRequestDto.Adapt<Review>();
            var updatedReview = await _unitOfWork.reviewRepository.UpdateAsync(id, review);
            _unitOfWork.Complete();

            var reviewResponseDto = updatedReview.Adapt<ReviewResponseDto>();
            _logger.LogInformation("Review with ID {ReviewId} updated successfully.", updatedReview.ReviewId);
            return reviewResponseDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating review with ID {ReviewId}.", id);
            throw;
        }
    }

    public async Task<string> DeleteReviewAsync(int id)
    {
        try
        {
            _logger.LogInformation("Starting to delete review with ID {ReviewId}.", id);

            var result = await _unitOfWork.reviewRepository.DeleteAsync(id);
            _unitOfWork.Complete();

            _logger.LogInformation("Review with ID {ReviewId} deleted successfully.", id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting review with ID {ReviewId}.", id);
            throw;
        }
    }

    public async Task<ReviewResponseDto> GetReviewByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Fetching review with ID {ReviewId}.", id);

            var review = await _unitOfWork.reviewRepository.GetByID(id);
            var reviewResponseDto = review.Adapt<ReviewResponseDto>();

            _logger.LogInformation("Review with ID {ReviewId} fetched successfully.", id);
            return reviewResponseDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching review with ID {ReviewId}.", id);
            throw;
        }
    }

    public async Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all reviews.");

            var reviews = await _unitOfWork.reviewRepository.GetAllAsync();
            var reviewResponseDtos = reviews.Adapt<IEnumerable<ReviewResponseDto>>(); 

            _logger.LogInformation("Fetched all reviews successfully.");
            return reviewResponseDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all reviews.");
            throw;
        }
    }
}
