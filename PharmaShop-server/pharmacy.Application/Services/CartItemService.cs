using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.CartItem;
using pharmacy.Core.Entities;

namespace pharmacy.Application.Services;
public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IMapper _mapper;

    public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
    }

    public async Task<CartItemResponseDto> AddCartItemAsync(CartItemRequestDto cartItemRequestDto)
    {
        var cartItem = _mapper.Map<CartItem>(cartItemRequestDto);
        var addedCartItem = await _cartItemRepository.CreateAsync(cartItem);
        return _mapper.Map<CartItemResponseDto>(addedCartItem);
    }

    public async Task<CartItemResponseDto> UpdateCartItemAsync(int id, CartItemRequestDto cartItemRequestDto)
    {
        var cartItem = await _cartItemRepository.GetByID(id);
        if (cartItem == null) return null;

        _mapper.Map(cartItemRequestDto, cartItem);
        var updatedCartItem = await _cartItemRepository.UpdateAsync(id,cartItem);
        return _mapper.Map<CartItemResponseDto>(updatedCartItem);
    }

    public async Task<string> DeleteCartItemAsync(int id)
    {
        await _cartItemRepository.DeleteAsync(id);
        return "Deleted success";
    }
}