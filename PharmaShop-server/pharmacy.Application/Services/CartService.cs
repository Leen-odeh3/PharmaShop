using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.Entities;
using pharmacy.Core;
namespace pharmacy.Application.Services;
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CartResponseDto> AddCartAsync(CartRequestDto cartRequestDto)
    {
        var cart = _mapper.Map<Cart>(cartRequestDto);
        cart.AddedDate = DateTime.Now;
        var addedCart = await _unitOfWork.cartRepository.CreateAsync(cart);
        _unitOfWork.Complete();
        return _mapper.Map<CartResponseDto>(addedCart);
    }

    public async Task<CartResponseDto> UpdateCartAsync(int id, CartRequestDto cartRequestDto)
    {
        var cart = await _unitOfWork.cartRepository.GetByID(id);
        if (cart == null) return null;

        _mapper.Map(cartRequestDto, cart);
        var updatedCart = await _unitOfWork.cartRepository.UpdateAsync(id,cart);
        _unitOfWork.Complete();
        return _mapper.Map<CartResponseDto>(updatedCart);
    }

    public async Task<CartResponseDto> GetCartByIdAsync(int id)
    {
        var cart = await _unitOfWork.cartRepository.GetByID(id);
        return _mapper.Map<CartResponseDto>(cart);
    }

    public async Task<string> DeleteCartAsync(int id)
    {
        await _unitOfWork.cartRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return "Deleted success";
    }
}