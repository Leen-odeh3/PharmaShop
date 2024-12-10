namespace pharmacy.Core.Entities;
public class WishlistItem : BaseEntity
{
    public string UserEmail { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

}
