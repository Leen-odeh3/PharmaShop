using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        if (images == null || !images.Any())
        {
            return BadRequest("No images provided.");
        }

        var uploadResults = await _unitOfWork.photoService.UploadImagesAsync(images);

        if (uploadResults?.Any() == true)
        {
            var imageUrls = new List<string>();
            var imagePublicIds = new List<string>();

            foreach (var result in uploadResults)
            {
                if (result?.Url != null && result?.PublicId != null)
                {
                    imageUrls.Add(result?.Url?.ToString());
                    imagePublicIds.Add(result?.PublicId);
                }
            }

            if (imageUrls.Count == 0 || imagePublicIds.Count == 0)
            {
                return BadRequest("No valid images uploaded.");
            }

            var category = new Category
            {
                CategoryName = Dto.CategoryName,
                CategoryDescription = Dto.CategoryDescription,
                ImageUrlsJson = imageUrls.Any() ? System.Text.Json.JsonSerializer.Serialize(imageUrls) : "[]",  
                ImagePublicIdsJson = imagePublicIds.Any() ? System.Text.Json.JsonSerializer.Serialize(imagePublicIds) : "[]", 
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

        var categoryDto = _mapper.Map<CategoryResponseDto>(category);

        return _responseHandler.Success(categoryDto, "Category retrieved successfully.");
    }

    [HttpGet("get-categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _unitOfWork.categoryRepository.GetAllAsync();

        if (categories == null || !categories.Any())
        {
            return NotFound("No categories found.");
        }
        var categoryDtos = _mapper.Map<List<CategoryResponseDto>>(categories);

        return Ok(new
        {
            statusCode = 200,
            succeeded = true,
            message = "Categories retrieved successfully.",
            data = categoryDtos
        });
    }


}
