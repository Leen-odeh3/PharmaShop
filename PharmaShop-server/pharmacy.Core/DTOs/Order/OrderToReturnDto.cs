
using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Core.DTOs.Order;
public class OrderToReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderTime { get; set; }
    public string status { get; set; }
    public Address ShippingAddress { get; set; }
    public string deliveryMethod { get; set; }
    public decimal Cost { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
    public decimal SubTotal { get; set; } 
    public decimal Total { get; set; } 
    public string PaymentIntentId { get; set; } = string.Empty;
}