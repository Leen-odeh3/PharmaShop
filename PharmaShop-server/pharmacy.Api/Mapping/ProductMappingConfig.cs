using AutoMapper;
using Mapster;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using System.Text.Json;

namespace pharmacy.Api.Mapping;
public class ProductMappingConfig
{
    public static void Configure()
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };

        TypeAdapterConfig<ProductRequestDto, Product>.NewConfig();

        TypeAdapterConfig<Product, ProductResponseDto>.NewConfig()
            .Map(dest => dest.ImageUrls, src =>
                string.IsNullOrEmpty(src.ImageUrlsJson)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(src.ImageUrlsJson, options))
            .Map(dest => dest.ImagePublicIds, src =>
                string.IsNullOrEmpty(src.ImagePublicIdsJson)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(src.ImagePublicIdsJson, options))
            .Map(dest => dest.Percentage, src => src.Discount.Percentage)
            .Map(dest => dest.Category, src => src.Category.CategoryName)
            .Map(dest => dest.Brand, src => src.Brand.BrandName);
    }
}


