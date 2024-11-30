using System.ComponentModel.DataAnnotations.Schema;

namespace pharmacy.Core.Entities;
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Discount { get; set; }
    public int Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
    public List<string> ImagePublicIds { get; set; } = new List<string>();
}