using AutoMapper;
using pharmacy.Core.DTOs.Discount;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class DiscountProfile :Profile
{
    public DiscountProfile()
    {
        CreateMap<DiscountRequestDto, Discount>();
        CreateMap<Discount,DiscountResponseDto>();

    }
}
