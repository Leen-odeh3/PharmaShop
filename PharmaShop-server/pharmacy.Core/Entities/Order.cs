
namespace pharmacy.Core.Entities;
public class Order
{
    public int OrderID { get; set; }
    public int CustomerID { get; set; } 
    public DateTime OrderDate { get; set; } =DateTime.Now;
    public decimal TotalAmount { get; set; } 
    public List<OrderItem> OrderItems { get; set; }  
}