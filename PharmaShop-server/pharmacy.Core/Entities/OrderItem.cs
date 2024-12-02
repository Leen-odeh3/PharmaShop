
namespace pharmacy.Core.Entities;
public class OrderItem
{
    public int OrderItemID { get; set; }
    public int ProductID { get; set; }  
    public int Quantity { get; set; }   
    public decimal Price { get; set; } 
    public decimal DiscountedPrice { get; set; } 
}