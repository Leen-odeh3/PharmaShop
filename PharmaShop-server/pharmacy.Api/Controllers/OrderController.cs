using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;
using AutoMapper;

namespace pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto is null)
            {
                return _responseHandler.BadRequest("Invalid order data.");
            }

            var order = _mapper.Map<Order>(orderRequestDto); 

            var orderResponseDto = await _unitOfWork.orderRepository.CreateAsync(order);
            _unitOfWork.Complete();
            return _responseHandler.Created(orderResponseDto, "Order created successfully.");
        }

        // PUT api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto is null)
            {
                return _responseHandler.BadRequest("Invalid order data.");
            }

            var order = _mapper.Map<Order>(orderRequestDto);
            //order.OrderID = id;

            var updatedOrder = await _unitOfWork.orderRepository.UpdateAsync(id, order);
            _unitOfWork.Complete();

            if (updatedOrder is null)
            {
                return _responseHandler.NotFound("Order not found.");
            }

            return _responseHandler.Success(updatedOrder, "Order updated successfully.");
        }

        // DELETE api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _unitOfWork.orderRepository.DeleteAsync(id);
            _unitOfWork.Complete();
            return _responseHandler.Success(result, "Order deleted successfully.");
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _unitOfWork.orderRepository.GetOrderWithDetailsAsync(id); // Fetching Order with OrderItems
            if (order is null)
            {
                return _responseHandler.NotFound("Order not found.");
            }

            return _responseHandler.Success(order, "Order retrieved successfully.");
        }

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _unitOfWork.orderRepository.GetAllAsync();
            return _responseHandler.Success(orders, "Orders retrieved successfully.");
        }
    }
}
