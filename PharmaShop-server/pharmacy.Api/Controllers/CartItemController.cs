using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.CartItem;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public CartItemController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItemRequestDto cartItemRequestDto)
    {
        if (cartItemRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid cart item data.");
        }

        var cartItem = _mapper.Map<CartItem>(cartItemRequestDto);
        var cartItemResponseDto = await _unitOfWork.cartItemRepository.CreateAsync(cartItem);
        _unitOfWork.Complete();
        return _responseHandler.Created(cartItemResponseDto, "Cart item created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemRequestDto cartItemRequestDto)
    {
        if (cartItemRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid cart item data.");
        }

        var cartItem = _mapper.Map<CartItem>(cartItemRequestDto);
       // cartItem.CartItemId = id;

        var updatedCartItem = await _unitOfWork.cartItemRepository.UpdateAsync(id, cartItem);
        _unitOfWork.Complete();

        if (updatedCartItem == null)
        {
            return _responseHandler.NotFound("Cart item not found.");
        }

        return _responseHandler.Success(updatedCartItem, "Cart item updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        var result = await _unitOfWork.cartItemRepository.DeleteAsync(id);
        _unitOfWork.Complete();

        return _responseHandler.Success(result, "Cart item deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartItemById(int id)
    {
        var cartItem = await _unitOfWork.cartItemRepository.GetByID(id);
        if (cartItem == null)
        {
            return _responseHandler.NotFound("Cart item not found.");
        }

        return _responseHandler.Success(cartItem, "Cart item retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCartItems()
    {
        var cartItems = await _unitOfWork.cartItemRepository.GetAllAsync();
        return _responseHandler.Success(cartItems, "Cart items retrieved successfully.");
    }
}
