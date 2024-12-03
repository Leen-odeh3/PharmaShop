using AutoMapper;
using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.DTOs.CartItem;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartRequestDto, Cart>();
        CreateMap<CartItemRequestDto, CartItem>();
        CreateMap<Cart, CartResponseDto>();
        CreateMap<CartItem, CartItemResponseDto>();
    }
}