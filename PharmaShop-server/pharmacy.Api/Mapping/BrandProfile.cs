using AutoMapper;
using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandRequestDto, Brand>();

        CreateMap<Brand, BrandGetResponse>();
    }

}
