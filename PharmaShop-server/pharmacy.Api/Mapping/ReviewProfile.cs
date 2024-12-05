using AutoMapper;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewRequestDto, Review>();
        CreateMap<Review, ReviewResponseDto>();

        CreateMap<Review, ReviewResponseDto>()
               .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}")) 
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)) 
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));
    }
}