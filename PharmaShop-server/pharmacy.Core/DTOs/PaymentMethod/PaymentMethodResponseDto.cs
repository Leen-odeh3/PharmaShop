
namespace pharmacy.Core.DTOs.PaymentMethod;
public class PaymentMethodResponseDto
{
    public long Id { get; set; }
    public string Provider { get; set; }
    public string AccountNumber { get; set; }
    public byte ExpiryMonth { get; set; }
    public short ExpiryYear { get; set; }
    public string SecurityCode { get; set; }
    public decimal TotalAmount { get; set; }
}
