using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public ReviewController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewRequestDto reviewRequestDto)
    {
        if (reviewRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid review data.");
        }

        var review = _mapper.Map<Review>(reviewRequestDto);
        var reviewResponseDto = await _unitOfWork.reviewRepository.CreateAsync(review);
        _unitOfWork.Complete();
        return _responseHandler.Created(reviewResponseDto, "Review created successfully.");
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewRequestDto reviewRequestDto)
    {
        if (reviewRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid review data.");
        }

        var review = _mapper.Map<Review>(reviewRequestDto);
      //  review.ReviewId = id;

        var updatedReview = await _unitOfWork.reviewRepository.UpdateAsync(id, review);
        _unitOfWork.Complete();

        if (updatedReview == null)
        {
            return _responseHandler.NotFound("Review not found.");
        }

        return _responseHandler.Success(updatedReview, "Review updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await _unitOfWork.reviewRepository.DeleteAsync(id);
        _unitOfWork.Complete();

        return _responseHandler.Success(result, "Review deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        var review = await _unitOfWork.reviewRepository.GetByID(id);
        if (review == null)
        {
            return _responseHandler.NotFound("Review not found.");
        }

        return _responseHandler.Success(review, "Review retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _unitOfWork.reviewRepository.GetAllAsync();
        return _responseHandler.Success(reviews, "Reviews retrieved successfully.");
    }
}
