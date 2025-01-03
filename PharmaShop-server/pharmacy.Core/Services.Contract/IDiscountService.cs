﻿using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Services.Contract;
public interface IDiscountService
{
    Task<DiscountResponseDto> CreateDiscountAsync(DiscountRequestDto discountRequestDto);
    Task<DiscountResponseDto> GetDiscountByIdAsync(int id);
    Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync();
    Task<DiscountResponseDto> UpdateDiscountAsync(int id, DiscountRequestDto discountRequestDto);
    Task<string> DeleteDiscountAsync(int id);
    Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now);
}