using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;
using pharmacy.Core;
using Mapster;

namespace pharmacy.Application.Services;
public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;

    public WishlistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProductResponseDto>> GetProductsFromWishlistByUserEmailAsync(string email)
    {
        var products = await _unitOfWork.WishlistRepo.GetAllProductForUserByEmailAsync(email);

        var productDtos = products?.Adapt<IReadOnlyList<ProductResponseDto>>();

        return productDtos ?? new List<ProductResponseDto>();
    }

    public async Task<bool> AddToWishlistAsync(string email, int productId)
    {
        var wishlistItem = await _unitOfWork.WishlistRepo.GetWishlistobjAsync(email, productId);
        if (wishlistItem is not null)
        {
            return false;
        }

        var newItem = new WishlistItem
        {
            UserEmail = email,
            ProductId = productId
        };

        await _unitOfWork.WishlistRepo.CreateAsync(newItem);

        var result =_unitOfWork.Complete();

        return result > 0;
    }

    public async Task<bool> RemoveFromWishlistAsync(string email, int productId)
    {
        var wishlistItem = await _unitOfWork.WishlistRepo.GetWishlistobjAsync(email, productId);
        if (wishlistItem is null)
        {
            return false;
        }

        await _unitOfWork.WishlistRepo.DeleteAsync(wishlistItem.WishlistItemId);

        var result =_unitOfWork.Complete();

        return result > 0;
    }
}