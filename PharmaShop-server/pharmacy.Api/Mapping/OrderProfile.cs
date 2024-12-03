using AutoMapper;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDto>()
                    .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderRequestDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemResponseDTO>();
         

        CreateMap<OrderItemRequestDTO, OrderItem>();
          
    }
}
