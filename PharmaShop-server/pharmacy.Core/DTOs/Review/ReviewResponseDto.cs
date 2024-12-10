
namespace pharmacy.Core.DTOs.Review;
public class ReviewResponseDto
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Email { get; set; }
    public string ProductName { get; set; }
}
