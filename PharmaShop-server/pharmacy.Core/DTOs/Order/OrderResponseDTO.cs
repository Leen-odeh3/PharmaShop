
namespace pharmacy.Core.DTOs.Order;
public class OrderResponseDTO
{
    public int OrderID { get; set; }  
    public int CustomerID { get; set; } 
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }  
    public List<OrderItemResponseDTO> OrderItems { get; set; } 
}