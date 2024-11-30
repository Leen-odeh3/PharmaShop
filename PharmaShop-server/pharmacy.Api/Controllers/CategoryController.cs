using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
   private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;


    public CategoryController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper,IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromForm] CategoryRequestDto Dto, [FromForm] List<IFormFile> images)
    {
        var uploadResults = await _photoService.UploadImagesAsync(images);

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
        // Retrieve category by ID
        var category = await _unitOfWork.categoryRepository.GetByID(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        if (category.ImagePublicIds != null && category.ImagePublicIds.Any())
        {
            foreach (var publicId in category.ImagePublicIds)
            {
                var deleteResult = await _photoService.DeleteImageAsync(publicId);
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



    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto categoryRequestDto)
    {
        if (categoryRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var category= _mapper.Map<Category>(categoryRequestDto);

        var categoryResponseDto = await _unitOfWork.categoryRepository.CreateAsync(category);
        _unitOfWork.Complete();
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

        var updatedcategory = await _unitOfWork.categoryRepository.UpdateAsync(category);
        _unitOfWork.Complete();
        if (updatedcategory is null)
        {
            return _responseHandler.NotFound("Category not found.");
        }

        return _responseHandler.Success(updatedcategory, "Category updated successfully.");
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

        return _responseHandler.Success(category, "Category retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var categories = await _unitOfWork.categoryRepository.GetAllAsync();
        return _responseHandler.Success(categories, "Categorys retrieved successfully.");
    }
}
