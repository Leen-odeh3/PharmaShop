

namespace pharmacy.Core.DTOs.Order;
public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Qunatity { get; set; }
    public decimal Price { get; set; }
}