using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Services.Contract;
using pharmacy.Core.Exceptions; 

namespace pharmacy.Api.Controllers
{
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
            if (discountRequestDto is null)
                throw new BadRequestException("Invalid discount data.");

            var createdDiscount = await _discountService.CreateDiscountAsync(discountRequestDto);
            return _responseHandler.Created(createdDiscount, "Discount created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountByIdAsync(int id)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id);

            if (discount is null)
             throw new NotFoundException($"Discount with ID {id} not found.");
          
            return _responseHandler.Success(discount, "Discount details fetched successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountsAsync()
        {
            var discounts = await _discountService.GetAllDiscountsAsync();

            if (discounts is null || !discounts.Any())
                throw new NotFoundException("No discounts found.");

            return _responseHandler.Success(discounts, "Discounts fetched successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscountAsync(int id, [FromBody] DiscountRequestDto discountRequestDto)
        {
            if (discountRequestDto is null)
                throw new BadRequestException("Invalid discount data.");

            var updatedDiscount = await _discountService.UpdateDiscountAsync(id, discountRequestDto);

            if (updatedDiscount is null)
                throw new NotFoundException($"Discount with ID {id} not found.");

            return _responseHandler.Success(updatedDiscount, "Discount updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountAsync(int id)
        {
            var result = await _discountService.DeleteDiscountAsync(id);
            return _responseHandler.Success("Discount deleted successfully.", result);
        }

        [HttpGet("top-discounts")]
        public async Task<IActionResult> GetTopDiscountsAsync(int topN, DateTime now)
        {
            var topDiscounts = await _discountService.GetTopDiscountsAsync(topN, now);

            if (topDiscounts is null || !topDiscounts.Any())
                throw new BadRequestException("No discounts found for the given time, please choose another date.");

            return _responseHandler.Success(topDiscounts, $"Top {topN} discounts fetched successfully.");
        }
    }
}
