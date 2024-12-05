using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs;
using pharmacy.Core.Entities;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.PaymentMethod;

namespace pharmacy.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<PaymentMethodResponseDto> AddPaymentMethodAsync(PaymentMethodRequestDto paymentMethodRequestDto)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodRequestDto);
            await _paymentMethodRepository.CreateAsync(paymentMethod);
            var paymentMethodResponseDto = _mapper.Map<PaymentMethodResponseDto>(paymentMethod);
            return paymentMethodResponseDto;
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodsAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            var paymentMethodResponseDtos = _mapper.Map<IEnumerable<PaymentMethodResponseDto>>(paymentMethods);
            return paymentMethodResponseDtos;
        }

        public async Task<PaymentMethodResponseDto> GetPaymentMethodByOrderIdAsync(int orderId)
        {
            var paymentMethod = await _paymentMethodRepository.GetPaymentMethodByOrderIdAsync(orderId);
            if (paymentMethod == null)
                return null;

            var paymentMethodResponseDto = _mapper.Map<PaymentMethodResponseDto>(paymentMethod);
            return paymentMethodResponseDto;
        }

        public async Task<PaymentMethod> UpdatePaymentMethodAsync(int id, PaymentMethodRequestDto paymentMethod)
        {
            var existingPaymentMethod = await _paymentMethodRepository.GetByID(id);
            if (existingPaymentMethod is null) return null;

            _mapper.Map(paymentMethod, existingPaymentMethod);
            await _paymentMethodRepository.UpdateAsync(id,existingPaymentMethod);
            return existingPaymentMethod;
        }
        public async Task<string> DeletePaymentMethodAsync(int id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByID(id);
            if (paymentMethod is null)
                return "NotFound entity";

            await _paymentMethodRepository.DeleteAsync(id); 
            return "Deleted success";
        }

    }
}
