
namespace pharmacy.Core.DTOs.Discount;
public class DiscountRequestDto
{
    public DateTime StartDateUtc { get; set; }
    public DateTime EndDateUtc { get; set; }
    public decimal Percentage { get; set; }
}
