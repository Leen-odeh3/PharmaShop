using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;
namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper; 

    public DiscountController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper; 
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountAsync([FromBody] DiscountRequestDto discountRequestDto)
    {
        try
        {
            if (discountRequestDto == null)
            {
                return _responseHandler.BadRequest("Invalid discount data.");
            }

            var discount = _mapper.Map<Discount>(discountRequestDto);

            var createdDiscount = await _unitOfWork.discountRepository.CreateAsync(discount);
            _unitOfWork.Complete();

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
            var discount = await _unitOfWork.discountRepository.GetByID(id);

            if (discount == null)
                return _responseHandler.NotFound($"Discount with ID {id} not found.");
            var discountResponseDto = _mapper.Map<DiscountResponseDto>(discount);

            return _responseHandler.Success(discountResponseDto, "Discount details fetched successfully.");
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
            var discounts = await _unitOfWork.discountRepository.GetAllAsync();

            if (discounts == null || !discounts.Any())
            {
                return _responseHandler.NotFound("No discounts found.");
            }

            var discountResponseDtos = _mapper.Map<IEnumerable<DiscountResponseDto>>(discounts);

            return _responseHandler.Success(discountResponseDtos, "Discounts fetched successfully.");
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
            var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);

            var updatedDiscount = _mapper.Map(discountRequestDto, existingDiscount);

            var result = await _unitOfWork.discountRepository.UpdateAsync(id, updatedDiscount);
            _unitOfWork.Complete();

            return _responseHandler.Success(result, "Discount updated successfully.");
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
            var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);

            if (existingDiscount == null)
            {
                return _responseHandler.NotFound($"Discount with ID {id} not found.");
            }

            var deleted = await _unitOfWork.discountRepository.DeleteAsync(id);
            _unitOfWork.Complete();

            return _responseHandler.NoContent("Discount deleted successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }



}
