using pharmacy.Core.DTOs.CartItem;

namespace pharmacy.Core.DTOs.Cart;
public class CartResponseDto
{
    public int CartId { get; set; }
    public string CustomerName { get; set; }
    public List<CartItemResponseDto> CartItems { get; set; }
    public decimal TotalAmount { get; set; }
}

