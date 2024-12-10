using Microsoft.AspNetCore.Http;
using pharmacy.Core.DTOs.Category;
using pharmacy.Core.Entities;
using pharmacy.Core;
using AutoMapper;
using Newtonsoft.Json;
using pharmacy.Core.Services.Contract;

namespace pharmacy.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }

    public async Task<CategoryResponseDto> AddCategoryAsync(CategoryRequestDto categoryRequestDto, List<IFormFile> images)
    {
        if (images == null || !images.Any())
        {
            return null; 
        }

        var uploadResults = await _photoService.UploadImagesAsync(images);
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
                return null;
            }

            var category = _mapper.Map<Category>(categoryRequestDto);

       //     category.ImageUrlsJson = imageUrls.Count > 0 ? JsonConvert.SerializeObject(imageUrls) : "[]";
          //  category.ImagePublicIdsJson = imagePublicIds.Count > 0 ? JsonConvert.SerializeObject(imagePublicIds) : "[]";
            await _unitOfWork.categoryRepository.CreateAsync(category);
            _unitOfWork.Complete();

            return _mapper.Map<CategoryResponseDto>(category);
        }

        return null;
    }

    public async Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryRequestDto categoryRequestDto)
    {
        var category = await _unitOfWork.categoryRepository.GetByID(id);

        _mapper.Map(categoryRequestDto, category);
        var updatedCategory = await _unitOfWork.categoryRepository.UpdateAsync(id, category);
        _unitOfWork.Complete();

        return _mapper.Map<CategoryResponseDto>(updatedCategory);
    }

    public async Task<string> DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork.categoryRepository.GetByID(id);
        if (category is null)
        {
            return "NotFound Category";
        }

        await _unitOfWork.categoryRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return "Deleted Success";
    }

    public async Task<CategoryResponseDto> GetCategoryByIdAsync(int id)
    {
        var category = await _unitOfWork.categoryRepository.GetByID(id);
        return category is null ? null : _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var categories = await _unitOfWork.categoryRepository.GetAllAsync();
        return categories is null || !categories.Any() ? new List<CategoryResponseDto>() : _mapper.Map<List<CategoryResponseDto>>(categories);
    }
}