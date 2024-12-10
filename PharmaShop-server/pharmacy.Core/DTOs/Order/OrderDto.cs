
using pharmacy.Core.DTOs.Order;
using System.ComponentModel.DataAnnotations;
public class OrderDto
{

    [Required]
    public int deliveryMethodId { get; set; }
    public shippingaddreesDto shippingaddress { get; set; }

    [Required]
    public string basketid { get; set; }
}