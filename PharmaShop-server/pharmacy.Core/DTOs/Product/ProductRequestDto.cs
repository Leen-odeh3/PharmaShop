
namespace pharmacy.Core.DTOs.Product;
public class ProductRequestDto
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public int? DiscountId { get; set; }
}
