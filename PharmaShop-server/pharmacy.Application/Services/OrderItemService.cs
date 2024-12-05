using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Entities;
using AutoMapper;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Order;

namespace pharmacy.Core.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderItemResponseDto> GetByIdAsync(int id)
    {
        var orderItem = await _unitOfWork.orderItemRepository.GetByID(id);
        return _mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<IEnumerable<OrderItemResponseDto>> GetAllAsync()
    {
        var orderItems = await _unitOfWork.orderItemRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderItemResponseDto>>(orderItems);
    }

    public async Task<OrderItemResponseDto> CreateAsync(OrderItemRequestDto request)
    {
        var orderItem = _mapper.Map<OrderItem>(request);
        var createdOrderItem = await _unitOfWork.orderItemRepository.CreateAsync(orderItem);
        _unitOfWork.Complete();
        return _mapper.Map<OrderItemResponseDto>(createdOrderItem);
    }

    public async Task<OrderItemResponseDto> UpdateAsync(int id, OrderItemRequestDto request)
    {
        var existingOrderItem = await _unitOfWork.orderItemRepository.GetByID(id);
        _unitOfWork.Complete();
        if (existingOrderItem is null) throw new Exception("Order Item not found");

        _mapper.Map(request, existingOrderItem);
        var updatedOrderItem = await _unitOfWork.orderItemRepository.UpdateAsync(id,existingOrderItem);
        _unitOfWork.Complete();
        return _mapper.Map<OrderItemResponseDto>(updatedOrderItem);
    }

    public async Task<string> DeleteAsync(int id)
    {
        await _unitOfWork.orderItemRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return "Order Item deleted successfully";
    }
}
