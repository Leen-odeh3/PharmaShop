using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Controllers;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Services.Contract;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace pharmacy.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : BaseApiController
{
    private readonly IOrderService _orderServices;
    private readonly IMapper _mapper;
    private readonly IResponseHandler _responseHandler;

    public OrderController(IOrderService orderServices,
        IMapper mapper, IResponseHandler responseHandler)
    {
        _orderServices = orderServices;
        _mapper = mapper;
        _responseHandler = responseHandler;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto dto)
    {
        var Email = User.FindFirstValue(ClaimTypes.Email);


        var Shippingaddress = _mapper.Map<shippingaddreesDto, Address>(dto.shippingaddress);

        var Order = await _orderServices.CreateOrderAsync(Email, dto.deliveryMethodId, dto.basketid, Shippingaddress);
        return Ok(_mapper.Map<Order, OrderToReturnDto>(Order));
    }


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var orders = await _orderServices.GetAllOrdersForUserAsync(email);

        return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderForUser([Required] int id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var order = await _orderServices.GetOrderByIdforUserAsync(id, email);
     
        return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
    }

    [HttpGet("deliverymethod")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMthods()
    {
        var result = await _orderServices.GetAllDeliveryMethodsAsync();

        return Ok(result);
    }
}