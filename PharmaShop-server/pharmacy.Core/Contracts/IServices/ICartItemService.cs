using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.DTOs.CartItem;

namespace pharmacy.Core.Contracts.IServices;
public interface ICartItemService
{
    Task<CartItemResponseDto> AddCartItemAsync(CartItemRequestDto cartItemRequestDto);
    Task<CartItemResponseDto> UpdateCartItemAsync(int id, CartItemRequestDto cartItemRequestDto);
    Task<string> DeleteCartItemAsync(int id);
   // Task<CartItemResponseDto> GetCartItemByIdAsync(int id);
   // Task<CartItemResponseDto> GetAllCartItems();
}