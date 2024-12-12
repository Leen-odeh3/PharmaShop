
namespace pharmacy.Core.Entities;
public class Achieve
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public DateTime CeatedAt { get; set; } = DateTime.UtcNow;
}
