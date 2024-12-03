
namespace pharmacy.Core.DTOs.Order;
public class OrderItemRequestDTO
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}