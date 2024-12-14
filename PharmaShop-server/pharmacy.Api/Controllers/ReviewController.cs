using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Services.Contract;
using pharmacy.Core.Exceptions; 

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IResponseHandler _responseHandler;

    public ReviewController(IReviewService reviewService, IResponseHandler responseHandler)
    {
        _reviewService = reviewService;
        _responseHandler = responseHandler;
    }

    [HttpPost("add-review")]
    public async Task<IActionResult> AddReview([FromBody] ReviewRequestDto reviewRequestDto)
    {
        if (reviewRequestDto is null)
            throw new BadRequestException("Invalid review data.");

        var reviewResponseDto = await _reviewService.AddReviewAsync(reviewRequestDto);
        return _responseHandler.Created(reviewResponseDto, "Review created successfully.");
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetReviewsByProductId(int productId)
    {
        try
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return _responseHandler.Success(reviews, "Reviews for the product retrieved successfully.");
        }
        catch (NotFoundException ex)
        {
            return _responseHandler.NotFound(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewRequestDto reviewRequestDto)
    {
        if (reviewRequestDto is null)
            throw new BadRequestException("Invalid review data.");

        var updatedReview = await _reviewService.UpdateReviewAsync(id, reviewRequestDto);
        if (updatedReview is null)
           throw new NotFoundException("Review not found.");

        return _responseHandler.Success(updatedReview, "Review updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await _reviewService.DeleteReviewAsync(id);

        return _responseHandler.Success(result, "Review deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        if (review is null)
            throw new NotFoundException("Review not found.");

        return _responseHandler.Success(review, "Review retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        return _responseHandler.Success(reviews, "Reviews retrieved successfully.");
    }
}
