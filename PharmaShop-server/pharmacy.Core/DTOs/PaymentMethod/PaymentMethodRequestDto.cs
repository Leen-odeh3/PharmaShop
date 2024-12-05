namespace pharmacy.Core.DTOs;
public class PaymentMethodRequestDto
{
    public string Provider { get; set; }
    public string AccountNumber { get; set; }
    public byte ExpiryMonth { get; set; }
    public short ExpiryYear { get; set; }
    public string SecurityCode { get; set; }
    public decimal TotalAmount { get; set; }
    public int OrderId { get; set; }
    public int PaymentTypeId { get; set; }
}
