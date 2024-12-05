using AutoMapper;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<OrderRequestDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<Order, OrderResponseDto>();
        CreateMap<OrderRequestDto, OrderItem>();
        CreateMap<OrderItem, OrderResponseDto>();
    }
}
