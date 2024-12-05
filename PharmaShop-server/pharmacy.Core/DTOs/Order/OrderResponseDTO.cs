
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.Enums;
namespace pharmacy.Core.DTOs.Order;
public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CustomerId { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; }
}
