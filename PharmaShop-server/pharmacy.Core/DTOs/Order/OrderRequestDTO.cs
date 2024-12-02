
namespace pharmacy.Core.DTOs.Order;
public class OrderRequestDTO
{
    public int CustomerID { get; set; } 
    public DateTime OrderDate { get; set; } = DateTime.Now;  
    public List<OrderItemRequestDTO> OrderItems { get; set; }  

}