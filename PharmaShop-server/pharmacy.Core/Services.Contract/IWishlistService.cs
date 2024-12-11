using pharmacy.Core.DTOs.Product;

namespace pharmacy.Core.Services.Contract;
public interface IWishlistService
{
    Task<IReadOnlyList<ProductResponseDto>> GetProductsFromWishlistByUserEmailAsync(string email);
    Task<bool> AddToWishlistAsync(string email, int productId);
    Task<bool> RemoveFromWishlistAsync(string email, int productId);
}