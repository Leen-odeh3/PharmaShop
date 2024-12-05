namespace pharmacy.Core.DTOs.Order;
public class OrderItemResponseDto
{
    public int OrderItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}