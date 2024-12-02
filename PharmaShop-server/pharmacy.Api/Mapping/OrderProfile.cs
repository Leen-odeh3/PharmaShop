﻿using AutoMapper;
using pharmacy.Core.DTOs.Order;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDTO>()
                    .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderRequestDTO, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemResponseDTO>();
         

        CreateMap<OrderItemRequestDTO, OrderItem>();
          
    }
}