using AutoMapper;
using Newtonsoft.Json;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.ImageUrlsJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(src.ImageUrlsJson)))
            .ForMember(dest => dest.ImagePublicIds, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.ImagePublicIdsJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(src.ImagePublicIdsJson)));
    }
}
