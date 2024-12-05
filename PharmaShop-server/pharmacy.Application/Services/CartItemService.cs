using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.CartItem;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Application.Services;
public class CartItemService : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartItemService(IMapper mapper,IUnitOfWork unitOfWork)
    {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
    }

    public async Task<CartItemResponseDto> AddCartItemAsync(CartItemRequestDto cartItemRequestDto)
    {
        var cartItem = _mapper.Map<CartItem>(cartItemRequestDto);
        var addedCartItem = await _unitOfWork.cartItemRepository.CreateAsync(cartItem);
        _unitOfWork.Complete();
        
        return _mapper.Map<CartItemResponseDto>(addedCartItem);
    }

    public async Task<CartItemResponseDto> UpdateCartItemAsync(int id, CartItemRequestDto cartItemRequestDto)
    {
        var cartItem = await _unitOfWork.cartItemRepository.GetByID(id);
        if (cartItem == null) return null;

        _mapper.Map(cartItemRequestDto, cartItem);
        var updatedCartItem = await _unitOfWork.cartItemRepository.UpdateAsync(id,cartItem);
        _unitOfWork.Complete();

        return _mapper.Map<CartItemResponseDto>(updatedCartItem);
    }

    public async Task<string> DeleteCartItemAsync(int id)
    {
        await _unitOfWork.cartItemRepository.DeleteAsync(id);
        _unitOfWork.Complete();

        return "Deleted success";
    }
}