using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Repositories.Contract;

namespace pharmacy.Api.Controllers;


public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepositry;
    private readonly IMapper _mapper;

    public BasketController(
        IBasketRepository basketRepositry
        , IMapper mapper) 
    {
        _basketRepositry = basketRepositry;
        _mapper = mapper;
    }


    [HttpGet] //Get :api/Basket
    public async Task<ActionResult<CustomerBasket>> GetUserBasket(string id)
    {

        var basket = await _basketRepositry.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        var model = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

        var Basket = await _basketRepositry.UpdateBasketAsync(model);

        return Ok(Basket);
    }

    [HttpDelete]
    public async Task DeleteBasket(string id)
    {
        await _basketRepositry.DeleteBasketAsync(id);
    }


}