using pharmacy.Core.Entities.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace pharmacy.Core.Entities;
public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    [ForeignKey("Brand")]
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public Discount Discount { get; set; }
    public int? DiscountId { get; set; }
}