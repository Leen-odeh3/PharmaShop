
namespace pharmacy.Core.Entities;
public class Category : BaseEntity
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<Product> Products { get; set; }
}
