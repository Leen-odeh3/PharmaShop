using pharmacy.Core.DTOs.Cart;
namespace pharmacy.Core.Contracts.IServices;
public interface ICartService
{
    Task<CartResponseDto> AddCartAsync(CartRequestDto cartRequestDto);
    Task<CartResponseDto> UpdateCartAsync(int id, CartRequestDto cartRequestDto);
    Task<CartResponseDto> GetCartByIdAsync(int id);
   // Task<CartResponseDto> GetAllCarts();
    Task<string> DeleteCartAsync(int id);
}