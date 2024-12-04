using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Application.Services;
public class DiscountService : IDiscountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DiscountResponseDto> CreateDiscountAsync(DiscountRequestDto discountRequestDto)
    {
        var discount = _mapper.Map<Discount>(discountRequestDto);
        var createdDiscount = await _unitOfWork.discountRepository.CreateAsync(discount);
        _unitOfWork.Complete();
        return _mapper.Map<DiscountResponseDto>(createdDiscount);
    }

    public async Task<DiscountResponseDto> GetDiscountByIdAsync(int id)
    {
        var discount = await _unitOfWork.discountRepository.GetByID(id);
        if (discount is null)
            return null;
        return _mapper.Map<DiscountResponseDto>(discount);
    }

    public async Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync()
    {
        var discounts = await _unitOfWork.discountRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DiscountResponseDto>>(discounts);
    }

    public async Task<DiscountResponseDto> UpdateDiscountAsync(int id, DiscountRequestDto discountRequestDto)
    {
        var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);

        var updatedDiscount = _mapper.Map(discountRequestDto, existingDiscount);
        var result = await _unitOfWork.discountRepository.UpdateAsync(id, updatedDiscount);
        _unitOfWork.Complete();

        return _mapper.Map<DiscountResponseDto>(result);
    }
    async Task<string> IDiscountService.DeleteDiscountAsync(int id)
    {
        var existingDiscount = await _unitOfWork.discountRepository.GetByID(id);
        if (existingDiscount is null)
            return "NotFound Discount";

        await _unitOfWork.discountRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return "Deleted Success";
    }

    public async Task<bool> CheckDiscountIsActiveOrNot(int id)
    {
        return await _unitOfWork.discountRepository.CheckDiscountIsActiveOrNot(id);
    }

    public async Task<int> GetTotalPrice(int id)
    {
        return await _unitOfWork.discountRepository.GetTotalPrice(id);
    }

    public async Task<IEnumerable<Discount>> GetTopDiscountsAsync(int topN, DateTime now)
    {
        return await _unitOfWork.discountRepository.GetTopDiscountsAsync(topN, now);
    }
}