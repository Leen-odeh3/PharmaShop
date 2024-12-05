using AutoMapper;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;
namespace pharmacy.Api.Mapping;
public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        CreateMap<OrderItem, OrderItemResponseDto>();

        CreateMap<OrderItemRequestDto, OrderItem>()
            .ForMember(dest => dest.OrderItemId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Order, opt => opt.Ignore()) 
            .ForMember(dest => dest.Product, opt => opt.Ignore());
    }
}