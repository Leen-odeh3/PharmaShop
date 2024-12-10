using pharmacy.Core.Entities.Helpers;

namespace pharmacy.Core.Entities;
public class ProductItemOrdered :BaseEntity
{
    public ProductItemOrdered(int productId, string? productName)
{
    ProductId = productId;
    ProductName = productName;
}
public int ProductId { get; set; }
public string? ProductName { get; set; }
}