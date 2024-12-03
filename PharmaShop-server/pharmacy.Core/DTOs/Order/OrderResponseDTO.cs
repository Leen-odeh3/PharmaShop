
using pharmacy.Core.DTOs.Customer;

namespace pharmacy.Core.DTOs.Order;
public class OrderResponseDto
{
    public long Id { get; set; }
    public int OrderNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public decimal TotalPrice { get; set; }
    public string OrderStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public CustomerResponseDto Customer { get; set; }
    public List<OrderItemResponseDTO> OrderItems { get; set; }
}
