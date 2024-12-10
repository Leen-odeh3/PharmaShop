namespace pharmacy.Core.DTOs.Category;
public class CategoryResponseDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
