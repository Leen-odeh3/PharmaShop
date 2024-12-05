using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;
namespace pharmacy.Application.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByID(id);
        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }

    public async Task<OrderResponseDto> CreateAsync(OrderRequestDto request)
    {
        var orderEntity = _mapper.Map<Order>(request);
        orderEntity.CreatedDate = DateTime.UtcNow;

        var createdOrder = await _orderRepository.CreateAsync(orderEntity);
        return _mapper.Map<OrderResponseDto>(createdOrder);
    }


    public async Task<OrderResponseDto> UpdateAsync(int id, OrderRequestDto request)
    {
        var existingOrder = await _orderRepository.GetByID(id);
        if (existingOrder is null)
            throw new Exception("Order not found");

        _mapper.Map(request, existingOrder);
        await _orderRepository.UpdateAsync(id,existingOrder);

        return _mapper.Map<OrderResponseDto>(existingOrder);
    }


    public async Task<string> DeleteAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
        return "Deleted success";
    }
}
