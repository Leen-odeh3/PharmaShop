using pharmacy.Core.DTOs.Order;

namespace pharmacy.Core.Contracts.IServices;
public interface IOrderService
{
    Task<OrderResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<OrderResponseDto>> GetAllAsync();
    Task<OrderResponseDto> CreateAsync(OrderRequestDto request);
    Task<OrderResponseDto> UpdateAsync(int id, OrderRequestDto request);
    Task<string> DeleteAsync(int id);
}


