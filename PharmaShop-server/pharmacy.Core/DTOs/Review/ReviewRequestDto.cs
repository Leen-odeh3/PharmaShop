
namespace pharmacy.Core.DTOs.Review;
public class ReviewRequestDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public int ProductId { get; set; }
    public string CustomerId { get; set; }
}
