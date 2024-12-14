using Mapster;
using pharmacy.Core.DTOs.Review;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Mapping;
public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Review, ReviewResponseDto>.NewConfig()
      .Map(dest => dest.ProductName, src => src.Product.ProductName)
      .Map(dest => dest.Email, src => src.Customer.Email);

    }
}
