
namespace pharmacy.Core.DTOs.Product;
public class ProductResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Discount { get; set; }
    public int Price { get; set; }
    public String Category { get; set; }
    public String Brand { get; set; }
}