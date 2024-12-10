using AutoMapper;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using System.Text.Json;

namespace pharmacy.Api.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };
        CreateMap<ProductRequestDto, Product>();

        CreateMap<Product, ProductResponseDto>()
    .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.DiscountId)) 
    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName)) 
    .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName)); 


    }
}

