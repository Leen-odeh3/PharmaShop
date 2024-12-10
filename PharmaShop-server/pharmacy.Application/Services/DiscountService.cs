using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;
using pharmacy.Core;
using pharmacy.Core.Services.Contract;
using pharmacy.Core.ILogger;
using Mapster;

namespace pharmacy.Application.Services;
public class DiscountService : IDiscountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILog _logger;

    public DiscountService(IUnitOfWork unitOfWork, ILog logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<DiscountResponseDto> CreateDiscountAsync(DiscountRequestDto discountRequestDto)
    {
        try
        {
            var discount = discountRequestDto.Adapt<Discount>();
            var createdDiscount = await _unitOfWork.discountRepository.CreateAsync(discount);
            _unitOfWork.Complete();
            _logger.Log("Discount created successfully", "info");
            return createdDiscount.Adapt<DiscountResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while creating discount: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<DiscountResponseDto> GetDiscountByIdAsync(int id)
    {
        try
        {
            var discount = await _unitOfWork.discountRepository.GetByID(id);
            if (discount is null)
            {
                _logger.Log($"Discount with ID {id} not found", "warning");
                return null;
            }
            return discount.Adapt<DiscountResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while fetching discount by ID {id}: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync()
    {
        try
        {
            var discounts = await _unitOfWork.discountRepository.GetAllAsync();
            return discounts.Adapt<IEnumerable<DiscountResponseDto>>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while fetching all discounts: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<DiscountResponseDto> UpdateDiscountAsync(int id, DiscountRequestDto discountRequestDto)
    {
        try
        {
            var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);
            if (existingDiscount is null)
            {
                _logger.Log($"Discount with ID {id} not found", "warning");
                return null;
            }

            var updatedDiscount = discountRequestDto.Adapt(existingDiscount);
            var result = await _unitOfWork.discountRepository.UpdateAsync(id, updatedDiscount);
            _unitOfWork.Complete();
            _logger.Log($"Discount with ID {id} updated successfully", "info");

            return result.Adapt<DiscountResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while updating discount with ID {id}: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<string> DeleteDiscountAsync(int id)
    {
        try
        {
            var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);
            if (existingDiscount is null)
            {
                _logger.Log($"Discount with ID {id} not found", "warning");
                return "NotFound Discount";
            }

            await _unitOfWork.discountRepository.DeleteAsync(id);
            _unitOfWork.Complete();
            _logger.Log($"Discount with ID {id} deleted successfully", "info");
            return "Deleted Success";
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while deleting discount with ID {id}: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now)
    {
        try
        {
            return await _unitOfWork.discountRepository.GetTopDiscountsAsync(topN, now);
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while fetching top {topN} discounts: {ex.Message}", "error");
            throw;
        }
    }
}
