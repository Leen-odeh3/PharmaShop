

namespace pharmacy.Core.DTOs.Cart;
public class CartResponseDto
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public List<CartItemResponseDto> CartItems { get; set; } = new List<CartItemResponseDto>(); // سلة تحتوي على المنتجات
}

