
namespace pharmacy.Core.DTOs.Order;
public class OrderRequestDto
{
    public long CustomerId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public List<OrderItemRequestDTO> OrderItems { get; set; }
    public decimal TotalPrice { get; set; } 
}
