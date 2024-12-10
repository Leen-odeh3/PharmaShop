
namespace pharmacy.Core.Entities;
public class Review : BaseEntity
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
    public string CustomerId { get; set; }
    public Product Product { get; set; }
    public Customer Customer { get; set; }
}