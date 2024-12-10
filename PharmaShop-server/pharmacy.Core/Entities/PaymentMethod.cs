﻿using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Enums;
namespace pharmacy.Core.Entities;
public class PaymentMethod:BaseEntity
{
    public string Provider { get; set; }
    public string AccountNumber { get; set; }
    public byte ExpiryMonth { get; set; }
    public short ExpiryYear { get; set; }
    public string SecurityCode { get; set; }
    public decimal TotalAmount { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; }
    public PaymentType Type { get; set; }
}