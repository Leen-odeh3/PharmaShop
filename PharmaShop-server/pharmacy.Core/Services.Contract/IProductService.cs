using Microsoft.AspNetCore.Http;
using pharmacy.Core.DTOs.Product;

public interface IProductService
{
    Task<ProductResponseDto> AddProductAsync(ProductRequestDto productDto, List<IFormFile> images);
    Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productDto);
    Task<string> DeleteProductAsync(int id);
    Task<ProductResponseDto> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<List<string>> GetProductImagesAsync(int id);
    Task<IEnumerable<ProductResponseDto>> SearchProductsByNameAsync(string name);

}