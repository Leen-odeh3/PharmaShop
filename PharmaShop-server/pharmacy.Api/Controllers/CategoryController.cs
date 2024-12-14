using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Services.Contract;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IResponseHandler _responseHandler;

    public CategoryController(ICategoryService categoryService, IResponseHandler responseHandler)
    {
        _categoryService = categoryService;
        _responseHandler = responseHandler;
    }
  //  [Authorize(Roles = "Admin")]
    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromForm] CategoryRequestDto Dto)
    {
        try
        {
            var result = await _categoryService.AddCategoryAsync(Dto);
            return _responseHandler.Success(result, "Category added successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }
  //  [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto categoryRequestDto)
    {
        try
        {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryRequestDto);
            return _responseHandler.Success(result, "Category updated successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }
   // [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        return _responseHandler.Success(result, "Category deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }
        return _responseHandler.Success(category, "Category retrieved successfully.");
    }

    [HttpGet("get-products/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
    {
        try
        {
            var result = await _categoryService.GetProductsByCategoryIdAsync(categoryId);
            if (result is null || !result.Any())
            {
                return _responseHandler.NotFound("No products found for this category.");
            }

            return _responseHandler.Success(result, "Products retrieved successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest($"An error occurred: {ex.Message}");
        }
    }


    [HttpGet("get-categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        if (categories is null || !categories.Any())
        {
            return _responseHandler.NotFound("No categories found.");
        }
        return _responseHandler.Success(categories, "Categories retrieved successfully.");
    }
}