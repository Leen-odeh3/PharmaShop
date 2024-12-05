using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Order;

namespace pharmacy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ResponseHandler _responseHandler;

        public OrderController(IOrderService orderService, ResponseHandler responseHandler)
        {
            _orderService = orderService;
            _responseHandler = responseHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return _responseHandler.Success(orders, "Orders retrieved successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return _responseHandler.NotFound($"Order with ID {id} not found.");

            return _responseHandler.Success(order, "Order retrieved successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto request)
        {
            if (!ModelState.IsValid)
                return _responseHandler.BadRequest("Invalid request data.");

            var createdOrder = await _orderService.CreateAsync(request);
            return _responseHandler.Created(createdOrder, "Order created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderRequestDto request)
        {
            if (!ModelState.IsValid)
                return _responseHandler.BadRequest("Invalid request data.");

            var updatedOrder = await _orderService.UpdateAsync(id, request);
            if (updatedOrder is null)
                return _responseHandler.NotFound($"Order with ID {id} not found.");

            return _responseHandler.Success(updatedOrder, "Order updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var Deleted = await _orderService.DeleteAsync(id);

            return _responseHandler.Success(Deleted,"Order deleted successfully.");
        }
    }
}
