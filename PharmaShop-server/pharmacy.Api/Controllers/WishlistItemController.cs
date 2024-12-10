using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.Entities;
using pharmacy.Core;
using System.Security.Claims;
using pharmacy.Core.DTOs.Product;

namespace pharmacy.Api.Controllers;
public class WishlistItemController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishlistItemController(IUnitOfWork unitOfWork
        , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDto>>> GetProductsFromListByuserEmail()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var result = await _unitOfWork.WishlistRepo.GetAllProductForUserByEmailAsync(email);


        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponseDto>>(result));
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("{productid}")]
    public async Task<ActionResult> AddToWishlist(int productid)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var obj = new WishlistItem()
        {
            ProductId = productid,
            UserEmail = email
        };

        await _unitOfWork.WishlistRepo.CreateAsync(obj);

        var result = _unitOfWork.Complete();

        if (result > 0)
            return Ok();

        return BadRequest();

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{productid}")]
    public async Task<ActionResult> DeleteProductfromWishlist(int productid)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var Wishlistobj = await _unitOfWork.WishlistRepo.GetWishlistobjAsync(email, productid);

        _unitOfWork.WishlistRepo.DeleteAsync(productid);

        var result =_unitOfWork.Complete();

        if (result > 0)
            return Ok();

        return BadRequest();

    }
}