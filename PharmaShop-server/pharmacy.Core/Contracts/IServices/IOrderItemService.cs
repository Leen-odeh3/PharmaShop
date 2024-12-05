using pharmacy.Core.DTOs.Order;

namespace pharmacy.Core.Contracts.IServices;
public interface IOrderItemService
{
    Task<OrderItemResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<OrderItemResponseDto>> GetAllAsync();
    Task<OrderItemResponseDto> CreateAsync(OrderItemRequestDto request);
    Task<OrderItemResponseDto> UpdateAsync(int id, OrderItemRequestDto request);
    Task<string> DeleteAsync(int id);
}