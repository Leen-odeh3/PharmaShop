namespace pharmacy.Core.Entities;
public class Brand
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public ICollection<Product> Products { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
