namespace pharmacy.Core.DTOs.Order;
public class OrderItemResponseDTO
{
    public int OrderItemID { get; set; } 
    public int ProductID { get; set; }  
    public string ProductName { get; set; }  
    public int Quantity { get; set; } 
    public decimal Price { get; set; }  
    public decimal DiscountedPrice { get; set; }  
    public decimal TotalPrice { get; set; }  
}