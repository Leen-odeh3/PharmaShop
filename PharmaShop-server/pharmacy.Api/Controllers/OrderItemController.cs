using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Order;

namespace pharmacy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _service;
    private readonly ResponseHandler _responseHandler;

    public OrderItemController(IOrderItemService service, ResponseHandler responseHandler)
    {
        _service = service;
        _responseHandler = responseHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return _responseHandler.Success(result,"Get success");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return _responseHandler.Success(result,"Get success");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderItemRequestDto request)
    {
        var result = await _service.CreateAsync(request);
        return _responseHandler.Created(result,"added success");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderItemRequestDto request)
    {
        var result = await _service.UpdateAsync(id, request);
        return _responseHandler.Success(result,"updated success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return _responseHandler.Success(result, "deleted success");
    }
}
