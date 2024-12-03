

namespace pharmacy.Core.DTOs.Review;
public class ReviewRequestDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public long ProductId { get; set; }
    public long CustomerId { get; set; }
}
