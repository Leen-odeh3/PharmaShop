using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.CartItem;
using pharmacy.Core.Contracts.IServices;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;
    private readonly IResponseHandler _responseHandler;

    public CartItemController(ICartItemService cartItemService,IResponseHandler responseHandler)
    {
        _cartItemService = cartItemService;
        _responseHandler = responseHandler;
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItemRequestDto cartItemRequestDto)
    {
        var result = await _cartItemService.AddCartItemAsync(cartItemRequestDto);
        return _responseHandler.Success(result, "added success");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemRequestDto cartItemRequestDto)
    {
        var result = await _cartItemService.UpdateCartItemAsync(id, cartItemRequestDto);
        if (result is null) 
            return _responseHandler.NotFound("CartItem null");
        return _responseHandler.Success(result,"Updated success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        await _cartItemService.DeleteCartItemAsync(id);
        return _responseHandler.NoContent("Deleted success");
    }
}