
namespace pharmacy.Core.DTOs.Brand;
public class BrandGetResponse
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
