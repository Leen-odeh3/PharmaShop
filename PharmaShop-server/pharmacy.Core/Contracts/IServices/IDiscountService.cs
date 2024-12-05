using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts.IServices;
public interface IDiscountService
{
    Task<DiscountResponseDto> CreateDiscountAsync(DiscountRequestDto discountRequestDto);
    Task<DiscountResponseDto> GetDiscountByIdAsync(int id);
    Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync();
    Task<DiscountResponseDto> UpdateDiscountAsync(int id, DiscountRequestDto discountRequestDto);
    Task<string> DeleteDiscountAsync(int id);
    Task<bool> CheckDiscountIsActiveOrNot(int id);
    Task<int> GetTotalPrice(int id);
    Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now);
}