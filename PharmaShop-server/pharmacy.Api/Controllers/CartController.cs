using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;
using System.Collections.Generic;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("save-checkout")]
    public async Task<IActionResult> SaveCheckOut([FromBody] IEnumerable<CreateAchieve> achieves)
    {
        var response = await _cartService.SaveCheckoutHistory(achieves);
        if (!ModelState.IsValid)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] Checkout checkout)
    {
        var response = await _cartService.CheckOut(checkout);
        if (!ModelState.IsValid)
            return BadRequest(response.Message);

        return Ok(response);  
    }
}
