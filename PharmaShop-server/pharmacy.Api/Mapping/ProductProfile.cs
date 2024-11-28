using AutoMapper;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDto, Product>();

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName)) 
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName));
        }
    }
}
