using pharmacy.Core.DTOs.Category;
using pharmacy.Core.DTOs.Product;

namespace pharmacy.Core.Services.Contract;
public interface ICategoryService
{
    Task<CategoryResponseDto> AddCategoryAsync(CategoryRequestDto categoryRequestDto);
    Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryRequestDto categoryRequestDto);
    Task<string> DeleteCategoryAsync(int id);
    Task<CategoryResponseDto> GetCategoryByIdAsync(int id);
    Task<List<ProductResponseDto>> GetProductsByCategoryIdAsync(int categoryId);
    Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
}