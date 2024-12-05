using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Discount;
namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    private readonly IResponseHandler _responseHandler;

    public DiscountController(IDiscountService discountService, IResponseHandler responseHandler)
    {
        _discountService = discountService;
        _responseHandler = responseHandler;
    }

    [HttpPost("addDiscount")]
    public async Task<IActionResult> CreateDiscountAsync([FromBody] DiscountRequestDto discountRequestDto)
    {
        try
        {
            if (discountRequestDto is null)
            {
                return _responseHandler.BadRequest("Invalid discount data.");
            }

            var createdDiscount = await _discountService.CreateDiscountAsync(discountRequestDto);
            return _responseHandler.Created(createdDiscount, "Discount created successfully.");
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
            var discount = await _discountService.GetDiscountByIdAsync(id);

            if (discount == null)
                return _responseHandler.NotFound($"Discount with ID {id} not found.");

            return _responseHandler.Success(discount, "Discount details fetched successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDiscountsAsync()
    {
        try
        {
            var discounts = await _discountService.GetAllDiscountsAsync();

            if (discounts is null || !discounts.Any())
            {
                return _responseHandler.NotFound("No discounts found.");
            }

            return _responseHandler.Success(discounts, "Discounts fetched successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscountAsync(int id, [FromBody] DiscountRequestDto discountRequestDto)
    {
        try
        {
            var updatedDiscount = await _discountService.UpdateDiscountAsync(id, discountRequestDto);

            if (updatedDiscount is null)
                return _responseHandler.NotFound($"Discount with ID {id} not found.");

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
            var result = await _discountService.DeleteDiscountAsync(id);

            return _responseHandler.Success("Discount deleted successfully.",result);
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }
}