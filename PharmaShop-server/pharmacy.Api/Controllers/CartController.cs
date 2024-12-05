using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Api.Responses;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICartItemService _cartItemService;
    private readonly IResponseHandler _responseHandler;

    public CartController(ICartService cartService, ICartItemService cartItemService,IResponseHandler responseHandler)
    {
        _cartService = cartService;
        _cartItemService = cartItemService;
        _responseHandler = responseHandler;
    }

    [HttpPost]
    public async Task<IActionResult> AddCart([FromBody] CartRequestDto cartRequestDto)
    {
        var result = await _cartService.AddCartAsync(cartRequestDto);
        return _responseHandler.Created(result,"added success");
            }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCart(int id)
    {
        var result = await _cartService.GetCartByIdAsync(id);
        if (result is null)
            return _responseHandler.NotFound("Cart null");
        return _responseHandler.Success(result, "Get Cart success");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, [FromBody] CartRequestDto cartRequestDto)
    {
        var result = await _cartService.UpdateCartAsync(id, cartRequestDto);
        if (result is null)
            return _responseHandler.NotFound("Cart null");
        return _responseHandler.Success(result, "Updated success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        await _cartService.DeleteCartAsync(id);
        return _responseHandler.NoContent("Deleted success");
    }
}
