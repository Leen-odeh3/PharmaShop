using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Cart;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public CartController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCarts()
    {
        var carts = await _unitOfWork.cartRepository.GetAllAsync();
        return _responseHandler.Success(carts, "Carts retrieved successfully.");
    }

        [HttpPost]
    public async Task<IActionResult> AddCart([FromBody] CartRequestDto cartRequestDto)
    {
        if (cartRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid cart data.");
        }

        var cart = _mapper.Map<Cart>(cartRequestDto);
        var cartResponseDto = await _unitOfWork.cartRepository.CreateAsync(cart);
        _unitOfWork.Complete();
        return _responseHandler.Created(cartResponseDto, "Cart created successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartById(int id)
    {
        var cart = await _unitOfWork.cartRepository.GetByID(id);
        if (cart is null)
        {
            return _responseHandler.NotFound("Cart not found.");
        }

        return _responseHandler.Success(cart, "Cart retrieved successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, [FromBody] CartRequestDto cartRequestDto)
    {
        if (cartRequestDto == null)
        {
            return _responseHandler.BadRequest("Invalid cart data.");
        }

        var cart = _mapper.Map<Cart>(cartRequestDto);

        var updatedCart = await _unitOfWork.cartRepository.UpdateAsync(id, cart);
        _unitOfWork.Complete();

        if (updatedCart == null)
        {
            return _responseHandler.NotFound("Cart not found.");
        }

        return _responseHandler.Success(updatedCart, "Cart updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var result = await _unitOfWork.cartRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return _responseHandler.Success(result, "Cart deleted successfully.");
    }
}
