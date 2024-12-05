using AutoMapper;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Core.DTOs.Product;
using Microsoft.AspNetCore.Http;
using pharmacy.Core.Contracts.IServices;

namespace pharmacy.Core.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _photoService = photoService;
    }

    public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto productDto, List<IFormFile> images)
    {
        var uploadResults = await _photoService.UploadImagesAsync(images);
        if (uploadResults == null || uploadResults.Count == 0)
            throw new Exception("Failed to upload images.");

        var imageUrls = new List<string>();
        var imagePublicIds = new List<string>();

        foreach (var result in uploadResults)
        {
            imageUrls.Add(result.Url.ToString());
            imagePublicIds.Add(result.PublicId);
        }

        var product = _mapper.Map<Product>(productDto);
        product.ImageUrls = imageUrls;
        product.ImagePublicIds = imagePublicIds;
        product.ImageUrlsJson = imageUrls.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(imageUrls) : "[]";
        product.ImagePublicIdsJson = imagePublicIds.Count > 0 ? System.Text.Json.JsonSerializer.Serialize(imagePublicIds) : "[]";

        await _unitOfWork.productRepository.CreateAsync(product);
        _unitOfWork.Complete();

        var productResponseDto = _mapper.Map<ProductResponseDto>(product);
        return productResponseDto;
    }

    public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productDto)
    {
        var existingProduct = await _unitOfWork.productRepository.GetByID(id);
        if (existingProduct is null)
            throw new Exception("Product not found.");

        var updatedProduct = _mapper.Map(productDto, existingProduct);

        await _unitOfWork.productRepository.UpdateAsync(id, updatedProduct);
        _unitOfWork.Complete();

        var productResponseDto = _mapper.Map<ProductResponseDto>(updatedProduct);
        return productResponseDto;
    }

    public async Task<string> DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.productRepository.GetByID(id);
        if (product is null)
            throw new Exception("Product not found.");

        await _unitOfWork.productRepository.DeleteAsync(id);
         _unitOfWork.Complete();

        return "Product deleted successfully.";
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.productRepository.GetByID(id);
        if (product is null)
            throw new Exception("Product not found.");

        var productResponseDto = _mapper.Map<ProductResponseDto>(product);
        return productResponseDto;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.productRepository.GetAllAsync();
        var productResponseDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return productResponseDtos;
    }
}
