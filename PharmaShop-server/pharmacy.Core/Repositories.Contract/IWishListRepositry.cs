using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IWishListRepositry : IGenericRepository<WishlistItem>
{
    public Task<IReadOnlyList<Product>?> GetAllProductForUserByEmailAsync(string email);
    Task<WishlistItem?> GetWishlistobjAsync(string email, int productid);
}