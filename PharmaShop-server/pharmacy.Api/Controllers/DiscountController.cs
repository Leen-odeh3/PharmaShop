using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Entities;
namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;

    public DiscountController(IUnitOfWork unitOfWork, IResponseHandler responseHandler)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
    }

    [HttpGet("top/{topN}")]
    public async Task<IActionResult> GetTopDiscountsAsync(int topN)
    {
        try
        {
            var discounts = await _unitOfWork.discountRepository.GetTopDiscountsAsync(topN, DateTime.UtcNow);

            if (discounts == null || discounts.Count() == 0)
            {
                return _responseHandler.NotFound("No active discounts found.");
            }

            return _responseHandler.Success(discounts, "Top discounts fetched successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountByIdAsync(int id)
    {
        try
        {
            var discount = await _unitOfWork.discountRepository.GetByID(id);

            if (discount == null)
                return _responseHandler.NotFound($"Discount with ID {id} not found.");

            return _responseHandler.Success(discount, "Discount details fetched successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountAsync([FromBody] Discount discount)
    {
        try
        {
            if (discount is null)
            {
                return _responseHandler.BadRequest("Invalid discount data.");
            }

            var createdDiscount = await _unitOfWork.discountRepository.CreateAsync(discount);

            return _responseHandler.Created(createdDiscount, "Discount created successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscountAsync(int id, [FromBody] Discount discount)
    {
        try
        {
            if (discount == null || discount.DiscountId != id)
            {
                return _responseHandler.BadRequest("Invalid discount data or ID mismatch.");
            }

            var updatedDiscount = await _unitOfWork.discountRepository.UpdateAsync(id, discount);

            if (updatedDiscount == null)
            {
                return _responseHandler.NotFound($"Discount with ID {id} not found.");
            }

            return _responseHandler.Success(updatedDiscount, "Discount updated successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscountAsync(int id)
    {
        try
        {
            var deleted = await _unitOfWork.discountRepository.DeleteAsync(id);

            return _responseHandler.NoContent("Discount deleted successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }
}
