using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Entities;
using AutoMapper;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Order;

namespace pharmacy.Core.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderItemResponseDto> GetByIdAsync(int id)
    {
        var orderItem = await _repository.GetByID(id);
        return _mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemResponseDto>> GetAllAsync()
    {
        var orderItems = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }

    public async Task<OrderItemResponseDto> CreateAsync(OrderItemRequestDto request)
    {
        var orderItem = _mapper.Map<OrderItem>(request);
        var createdOrderItem = await _repository.CreateAsync(orderItem);
        return _mapper.Map<OrderItemResponseDto>(createdOrderItem);
    }

    public async Task<OrderItemResponseDto> UpdateAsync(int id, OrderItemRequestDto request)
    {
        var existingOrderItem = await _repository.GetByID(id);
        if (existingOrderItem is null) throw new Exception("Order Item not found");

        _mapper.Map(request, existingOrderItem);
        var updatedOrderItem = await _repository.UpdateAsync(id,existingOrderItem);
        return _mapper.Map<OrderItemResponseDto>(updatedOrderItem);
    }

    public async Task<string> DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return "Order Item deleted successfully";
    }
}
