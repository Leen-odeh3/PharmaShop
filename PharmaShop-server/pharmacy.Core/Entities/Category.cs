namespace pharmacy.Core.Entities;
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string ImageUrl { get; set; }
    public string ImagePublicId { get; set; }
    public ICollection<Product> Products { get; set; }
}