using pharmacy.Core.DTOs.CartItem;
namespace pharmacy.Core.DTOs.Cart;
public class CartRequestDto
{
    public string CustomerId { get; set; }
    public List<CartItemRequestDto> CartItems { get; set; }
}
