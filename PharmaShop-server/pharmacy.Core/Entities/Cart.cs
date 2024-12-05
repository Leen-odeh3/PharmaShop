
namespace pharmacy.Core.Entities;
public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime AddedDate { get; set; }
}
