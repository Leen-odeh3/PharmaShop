namespace pharmacy.Core.Entities;
public class WishlistItem 
{
    public int WishlistItemId { get; set; }
    public string UserEmail { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

}
