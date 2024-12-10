using AutoMapper;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Entities;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.DTOs.Order;

namespace pharmacy.Api.Helper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponseDto>()
            .ForMember(P => P.Brand, O => O.MapFrom(P => P.Brand.BrandName))
           .ForMember(P => P.Category, O => O.MapFrom(P => P.Category.CategoryName));



        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemsDto, BasketItem>();
        CreateMap<shippingaddreesDto,Address>();

        CreateMap<Order, OrderToReturnDto>()
            .ForMember(o => o.deliveryMethod, o => o.MapFrom(o => o.deliveryMethod.ShortName))
            .ForMember(o => o.Cost, o => o.MapFrom(o => o.deliveryMethod.Cost))
            .ForMember(o => o.status, o => o.MapFrom<OrderStatusResolver>());

     /*   CreateMap<OrderItem, OrderItemDto>()
             .ForMember(o => o.ProductName, o => o.MapFrom(o => o.ProductItemOrder.ProductName))
             .ForMember(o => o.ProductId, o => o.MapFrom(o => o.ProductItemOrder.ProductId));
     */

        CreateMap<AddressDto,Address>().ReverseMap();
    }
}