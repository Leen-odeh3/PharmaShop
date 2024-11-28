using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.Repositories;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IResponseHandler responseHandler, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto categoryRequestDto)
    {
        if (categoryRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var category= _mapper.Map<Category>(categoryRequestDto);

        var categoryResponseDto = await _categoryRepository.CreateAsync(category);
        return _responseHandler.Created(categoryResponseDto, "Category created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CategoryRequestDto categoryRequestDto)
    {
        if (categoryRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var category = _mapper.Map<Category>(categoryRequestDto);

        var updatedcategory = await _categoryRepository.UpdateAsync(category);
        if (updatedcategory is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }

        return _responseHandler.Success(updatedcategory, "Category updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryRepository.DeleteAsync(id);
        return _responseHandler.Success(result, "Category deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetByID(id);
        if (category is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }

        return _responseHandler.Success(category, "Category retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _responseHandler.Success(categories, "Categorys retrieved successfully.");
    }
}
