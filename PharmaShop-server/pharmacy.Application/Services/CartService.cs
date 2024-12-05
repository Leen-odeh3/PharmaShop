using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.Entities;
namespace pharmacy.Application.Services;
public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<CartResponseDto> AddCartAsync(CartRequestDto cartRequestDto)
    {
        var cart = _mapper.Map<Cart>(cartRequestDto);
        cart.AddedDate = DateTime.Now;
        var addedCart = await _cartRepository.CreateAsync(cart);
        return _mapper.Map<CartResponseDto>(addedCart);
    }

    public async Task<CartResponseDto> UpdateCartAsync(int id, CartRequestDto cartRequestDto)
    {
        var cart = await _cartRepository.GetByID(id);
        if (cart == null) return null;

        _mapper.Map(cartRequestDto, cart);
        var updatedCart = await _cartRepository.UpdateAsync(id,cart);
        return _mapper.Map<CartResponseDto>(updatedCart);
    }

    public async Task<CartResponseDto> GetCartByIdAsync(int id)
    {
        var cart = await _cartRepository.GetByID(id);
        return _mapper.Map<CartResponseDto>(cart);
    }

    public async Task<string> DeleteCartAsync(int id)
    {
        await _cartRepository.DeleteAsync(id);
        return "Deleted success";
    }
}