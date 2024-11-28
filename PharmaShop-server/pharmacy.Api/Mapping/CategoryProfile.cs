using AutoMapper;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class CategoryProfile : Profile
{
    public CategoryProfile() 
    {
        CreateMap<CategoryRequestDto, Category>();
        CreateMap<Category, CategoryResponseDto>();
    }
}
