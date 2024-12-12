
namespace pharmacy.Core.DTOs.Product;
public class ProductResponseDto
{
    public int  ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Discount { get; set; }
    public int Price { get; set; }
    public String Category { get; set; }
    public String Brand { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
    public List<string> ImagePublicIds { get; set; } = new List<string>();
}