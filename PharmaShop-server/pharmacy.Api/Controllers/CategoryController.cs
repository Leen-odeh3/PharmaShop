using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;
using System.Text.Json;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
   private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;


    public CategoryController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromForm] CategoryRequestDto Dto, [FromForm] List<IFormFile> images)
    {
        var uploadResults = await _unitOfWork.photoService.UploadImagesAsync(images);

        if (uploadResults?.Any() == true)
        {
            var imageUrls = new List<string>();
            var imagePublicIds = new List<string>();

            foreach (var result in uploadResults)
            {
                imageUrls.Add(result?.Url?.ToString());
                imagePublicIds.Add(result?.PublicId);   
            }

            var category = new Category
            {
                CategoryName = Dto.CategoryName,
                CategoryDescription = Dto.CategoryDescription,
                ImageUrls = imageUrls, 
                ImagePublicIds = imagePublicIds, 
            };

            await _unitOfWork.categoryRepository.CreateAsync(category);
            _unitOfWork.Complete();

            return Ok(new { message = "Category added successfully", category });
        }

        return BadRequest("Failed to upload images.");
    }


    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> DeleteCategoryImages(int id)
    {
        var category = await _unitOfWork.categoryRepository.GetByID(id);
        if (category is null)
        {
            return NotFound("Category not found");
        }

        if (category.ImagePublicIds != null && category.ImagePublicIds.Any())
        {
            foreach (var publicId in category.ImagePublicIds)
            {
                var deleteResult = await _unitOfWork.photoService.DeleteImageAsync(publicId);
                if (deleteResult?.Result != "ok")
                {
                    return BadRequest($"Failed to delete image with PublicId: {publicId} from Cloudinary");
                }
            }
        }
        else
        {
            return BadRequest("No images found to delete for this category.");
        }

        var result = await _unitOfWork.categoryRepository.DeleteAsync(id);
        _unitOfWork.Complete();

        return Ok(new { message = "Category and its images deleted successfully" });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Updatecategory(int id, [FromBody] CategoryRequestDto categoryRequestDto)
    {
        if (categoryRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid category data.");
        }

        var category = await _unitOfWork.categoryRepository.GetByID(id);
        if (category is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }

        category.CategoryName = categoryRequestDto.CategoryName;
        category.CategoryDescription = categoryRequestDto.CategoryDescription;

        var updatedCategory = await _unitOfWork.categoryRepository.UpdateAsync(id, category);

        _unitOfWork.Complete();

        return _responseHandler.Success(updatedCategory, "Category updated successfully.");
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImageCategory(int id)
    {
        var result = await _unitOfWork.categoryRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return _responseHandler.Success(result, "Category deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _unitOfWork.categoryRepository.GetByID(id);

        if (category is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }

        var categoryDto = new CategoryResponseDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDescription = category.CategoryDescription,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
            ImageUrls = string.IsNullOrEmpty(category.ImageUrlsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(category.ImageUrlsJson),
            ImagePublicIds = string.IsNullOrEmpty(category.ImagePublicIdsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(category.ImagePublicIdsJson),
        };

        return _responseHandler.Success(categoryDto, "Category retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _unitOfWork.categoryRepository.GetAllAsync();

        if (categories == null || !categories.Any())
        {
            return _responseHandler.NotFound("No categories found.");
        }

        return _responseHandler.Success(categories, "Categories retrieved successfully.");
    }
}
