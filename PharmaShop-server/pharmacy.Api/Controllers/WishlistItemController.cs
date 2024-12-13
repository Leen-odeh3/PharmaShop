using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using pharmacy.Core.Services.Contract;
using pharmacy.Api.Responses;
using pharmacy.Core.Exceptions;  

namespace pharmacy.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WishlistItemController : BaseApiController
{
    private readonly IWishlistService _wishlistService;
    private readonly IResponseHandler _responseHandler;

    public WishlistItemController(IWishlistService wishlistService, IResponseHandler responseHandler)
    {
        _wishlistService = wishlistService;
        _responseHandler = responseHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsFromWishlist()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var products = await _wishlistService.GetProductsFromWishlistByUserEmailAsync(email);

        if (products.Count == 0)
            throw new NotFoundException("No products found in wishlist.");

        return _responseHandler.Success(products, "Products retrieved successfully.");
    }

    [HttpPost("{productid}")]
    public async Task<IActionResult> AddToWishlist(int productid)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var success = await _wishlistService.AddToWishlistAsync(email, productid);

        if (success)
            return _responseHandler.Created(productid, "Item successfully added to wishlist.");

        throw new BadRequestException("Item already in wishlist.");
    }

    [HttpDelete("{productid}")]
    public async Task<IActionResult> DeleteProductFromWishlist(int productid)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var success = await _wishlistService.RemoveFromWishlistAsync(email, productid);

        if (success)
            return _responseHandler.Success(success, "Item successfully removed from wishlist.");

        throw new NotFoundException("Item not found in wishlist.");
    }
}
