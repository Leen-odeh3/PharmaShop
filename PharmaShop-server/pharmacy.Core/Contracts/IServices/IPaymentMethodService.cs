using pharmacy.Core.DTOs;
using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.Entities;

namespace pharmacy.Core.Contracts.IServices;
public interface IPaymentMethodService
{
    Task<PaymentMethodResponseDto> AddPaymentMethodAsync(PaymentMethodRequestDto paymentMethod);
    Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodsAsync();
    Task<PaymentMethodResponseDto> GetPaymentMethodByOrderIdAsync(int orderId);
    Task<string> DeletePaymentMethodAsync(int id);
    Task<PaymentMethod> UpdatePaymentMethodAsync(int id, PaymentMethodRequestDto paymentMethod);
}