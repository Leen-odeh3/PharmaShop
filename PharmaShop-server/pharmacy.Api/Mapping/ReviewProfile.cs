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
    }
}