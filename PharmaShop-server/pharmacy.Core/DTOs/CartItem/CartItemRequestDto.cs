
namespace pharmacy.Core.DTOs.CartItem;
public class CartItemRequestDto
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
