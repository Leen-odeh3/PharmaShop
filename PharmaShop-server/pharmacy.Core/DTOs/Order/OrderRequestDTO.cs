
using pharmacy.Core.Enums;

namespace pharmacy.Core.DTOs.Order;
public class OrderRequestDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string CustomerId { get; set; }
    public List<OrderItemRequestDto> OrderItems { get; set; }
}
