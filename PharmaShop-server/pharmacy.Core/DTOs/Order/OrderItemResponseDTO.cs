namespace pharmacy.Core.DTOs.Order;
public class OrderItemResponseDTO
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } 
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; } 
}