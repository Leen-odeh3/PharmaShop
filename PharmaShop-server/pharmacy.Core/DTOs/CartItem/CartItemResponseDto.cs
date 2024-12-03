
namespace pharmacy.Core.DTOs.CartItem;
public class CartItemResponseDto
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } 
    public decimal Price { get; set; }  
}
