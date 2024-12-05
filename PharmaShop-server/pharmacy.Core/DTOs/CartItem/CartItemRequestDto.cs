
namespace pharmacy.Core.DTOs.CartItem;
public class CartItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
