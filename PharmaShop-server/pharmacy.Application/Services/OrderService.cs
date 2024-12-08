using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core;
using pharmacy.Core.Entities;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<OrderResponseDto> CreateAsync(OrderRequestDto request)
    {
        var order = _mapper.Map<Order>(request);

        await _unitOfWork.orderRepository.CreateAsync(order);

        _unitOfWork.Complete();

        return _mapper.Map<OrderResponseDto>(order);
    }


    public async Task<OrderResponseDto> GetByIdAsync(int id)
    {
        var order = await _unitOfWork.orderRepository.GetByID(id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _unitOfWork.orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }


    public async Task<OrderResponseDto> UpdateAsync(int id, OrderRequestDto request)
    {
        var existingOrder = await _unitOfWork.orderRepository.GetByID(id);
        if (existingOrder == null)
        {
            throw new Exception("Order not found");
        }

        _mapper.Map(request, existingOrder);
        await _unitOfWork.orderRepository.UpdateAsync(id, existingOrder);
        _unitOfWork.Complete();

        return _mapper.Map<OrderResponseDto>(existingOrder);
    }

    public async Task<string> DeleteAsync(int id)
    {
        var order = await _unitOfWork.orderRepository.GetByID(id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        await _unitOfWork.orderRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return "Deleted success";
    }
}
