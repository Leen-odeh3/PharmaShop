using AutoMapper;
using pharmacy.Core;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;

namespace pharmacy.Application.Services;
public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewResponseDto> AddReviewAsync(ReviewRequestDto reviewRequestDto)
    {
        var review = _mapper.Map<Review>(reviewRequestDto);
        var createdReview = await _unitOfWork.reviewRepository.CreateAsync(review);
        _unitOfWork.Complete();
        return _mapper.Map<ReviewResponseDto>(createdReview);
    }

    public async Task<ReviewResponseDto> UpdateReviewAsync(int id, ReviewRequestDto reviewRequestDto)
    {
        var review = _mapper.Map<Review>(reviewRequestDto);
        var updatedReview = await _unitOfWork.reviewRepository.UpdateAsync(id, review);
        _unitOfWork.Complete();
        return _mapper.Map<ReviewResponseDto>(updatedReview);
    }

    public async Task<string> DeleteReviewAsync(int id)
    {
        var result = await _unitOfWork.reviewRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return result;
    }

    public async Task<ReviewResponseDto> GetReviewByIdAsync(int id)
    {
        var review = await _unitOfWork.reviewRepository.GetByID(id);
        return _mapper.Map<ReviewResponseDto>(review);
    }

    public async Task<IEnumerable<ReviewResponseDto>> GetAllReviewsAsync()
    {
        var reviews = await _unitOfWork.reviewRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);
    }
}