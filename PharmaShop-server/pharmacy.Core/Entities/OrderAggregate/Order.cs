using pharmacy.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace pharmacy.Core.Entities.OrderAggregate;
public class Order : BaseEntity
{
    public Order()
    {
    }

    public Order(string buyerEmail, Address shippingAddress, int deliveryMethodid, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethodId = deliveryMethodid;
        Items = items;
        SubTotal = subTotal;
        PaymentIntentId = paymentIntentId;
    }

    public string BuyerEmail { get; set; }

    public DateTimeOffset OrderTime { get; set; } = DateTimeOffset.UtcNow;

    public OrderStatus status { get; set; } = OrderStatus.Pending;

    public Address ShippingAddress { get; set; }

    public DeliveryMethod deliveryMethod { get; set; }

    public int? DeliveryMethodId { get; set; } 

    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    public decimal SubTotal { get; set; }

    [NotMapped]
    public decimal Total => deliveryMethod.Cost + SubTotal; 
    public string PaymentIntentId { get; set; }
}