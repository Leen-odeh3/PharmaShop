namespace pharmacy.Core.DTOs.Discount;
public class DiscountResponseDto
{
    public int Id { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime EndDateUtc { get; set; }
    public decimal Percentage { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public bool IsActive { get; set; }
}
