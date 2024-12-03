using AutoMapper;
using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class PaymentMethodProfile : Profile
{
    public PaymentMethodProfile()
    {
        CreateMap<PaymentMethodRequestDto, PaymentMethod>();
        CreateMap<PaymentMethod, PaymentMethodResponseDto>();
    }
}