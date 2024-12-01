namespace pharmacy.Core.DTOs.Category;
public class CategoryResponseDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<string> ImageUrls { get; set; } = new List<string>();
    public List<string> ImagePublicIds { get; set; } = new List<string>();
}
