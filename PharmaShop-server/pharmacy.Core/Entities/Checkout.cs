namespace pharmacy.Core.Entities;
public class Checkout
{
    public Guid PaymentMethodId { get; set; }
    public IEnumerable<Cart> carts { get; set;}
}
