using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;
using pharmacy.Core;
using pharmacy.Core.Services.Contract;
using Mapster;
using pharmacy.Core.ILogger;
using pharmacy.Core.Exceptions;
using pharmacy.Core.DTOs.Product;

namespace pharmacy.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILog _logger; 

    public CategoryService(IUnitOfWork unitOfWork, ILog logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<ProductResponseDto>> GetProductsByCategoryIdAsync(int categoryId)
    {
        try
        {
            var products = await _unitOfWork.categoryRepository.GetProductsByCategoryIdAsync(categoryId);
            if (products is null || !products.Any())
            {
                _logger.Log("No products found for this category", "warning");
                return new List<ProductResponseDto>();
            }
            return products.Adapt<List<ProductResponseDto>>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while retrieving products by category: {ex.Message}", "error");
            throw;
        }
    }
    public async Task<CategoryResponseDto> AddCategoryAsync(CategoryRequestDto categoryRequestDto)
    {
        try
        {
            if (string.IsNullOrEmpty(categoryRequestDto.CategoryName))
                throw new BadRequestException("Category name is required.");

            var category = categoryRequestDto.Adapt<Category>(); 
            await _unitOfWork.categoryRepository.CreateAsync(category);
            _unitOfWork.Complete();

            _logger.Log("Category added successfully", "info");

            return category.Adapt<CategoryResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while adding category: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryRequestDto categoryRequestDto)
    {
        try
        {
            var category = await _unitOfWork.categoryRepository.GetByID(id);
            if (category is null)
                _logger.Log("Category not found for update", "warning");

            categoryRequestDto.Adapt(category); 
            var updatedCategory = await _unitOfWork.categoryRepository.UpdateAsync(id, category);
            _unitOfWork.Complete();

            _logger.Log("Category updated successfully", "info");

            return updatedCategory.Adapt<CategoryResponseDto>(); 
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while updating category: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<string> DeleteCategoryAsync(int id)
    {
        try
        {
            var category = await _unitOfWork.categoryRepository.GetByID(id);
            if (category is null)
            {
                _logger.Log("Category not found for deletion", "warning");
                return "NotFound Category";
            }

            await _unitOfWork.categoryRepository.DeleteAsync(id);
            _unitOfWork.Complete();

            _logger.Log("Category deleted successfully", "info");
            return "Deleted Success";
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while deleting category: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<CategoryResponseDto> GetCategoryByIdAsync(int id)
    {
        try
        {
            var category = await _unitOfWork.categoryRepository.GetByID(id);
            if (category is null)
                _logger.Log("Category not found", "warning");

            _logger.Log("Category retrieved successfully", "info");
            return category.Adapt<CategoryResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while retrieving category: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _unitOfWork.categoryRepository.GetAllAsync();
            if (categories is null || !categories.Any())
            {
                _logger.Log("No categories found", "warning");
                return new List<CategoryResponseDto>();
            }

            _logger.Log("Categories retrieved successfully", "info");
            return categories.Adapt<List<CategoryResponseDto>>(); 
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while retrieving all categories: {ex.Message}", "error");
            throw;
        }
    }
}
