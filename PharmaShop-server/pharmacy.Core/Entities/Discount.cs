
namespace pharmacy.Core.Entities;
public class Discount 
{
    public int DiscountId { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime EndDateUtc { get; set; }   
    public decimal Percentage { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public bool IsActive => StartDateUtc<=DateTime.Now && EndDateUtc>=DateTime.Now;
    public Discount()
    {
        CreatedAtUtc = DateTime.UtcNow;
    }
    public ICollection<Product> Products { get; set; }  
}
