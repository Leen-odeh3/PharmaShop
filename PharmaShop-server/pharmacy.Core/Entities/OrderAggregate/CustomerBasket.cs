﻿namespace pharmacy.Core.Entities.OrderAggregate;
public class CustomerBasket
{
    public CustomerBasket(string id)
    {
        Id = id;
        Items = new List<BasketItem>();
    }
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
}
