using pharmacy.Core.Entities;
using pharmacy.Core.DTOs.Product;
using Microsoft.AspNetCore.Http;
using pharmacy.Core.Services.Contract;
using pharmacy.Core.ILogger;
using Mapster;

namespace pharmacy.Core.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoService _photoService;
    private readonly ILog _logger;

    public ProductService(IUnitOfWork unitOfWork, IPhotoService photoService, ILog logger)
    {
        _unitOfWork = unitOfWork;
        _photoService = photoService;
        _logger = logger;
    }

    public async Task<List<string>> GetProductImagesAsync(int id)
    {
        _logger.Log($"Fetching images for product with ID: {id}.", "info");

        try
        {
            var images = await _photoService.GetImagesByProductIdAsync(id);

            if (images == null || images.Count == 0)
            {
                _logger.Log($"No images found for product with ID {id}.", "warning");
            }

            _logger.Log($"Images for product ID {id} retrieved successfully.", "info");
            return images;
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in GetProductImagesAsync: {ex.Message}", "error");
            throw;
        }
    }
public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto productDto, List<IFormFile> images)
    {
        _logger.Log("Starting AddProductAsync operation.", "info");

        try
        {
            var uploadResults = await _photoService.UploadImagesAsync(images);
            if (uploadResults is null || uploadResults.Count == 0)
            {
                _logger.Log("Failed to upload images.", "error");
                throw new Exception("Failed to upload images.");
            }

            var imageUrls = uploadResults.Select(result => result.Url.ToString()).ToList();
            var imagePublicIds = uploadResults.Select(result => result.PublicId).ToList();

            var product = productDto.Adapt<Product>();
            if (productDto.DiscountId.HasValue)
            {
                var discount = await _unitOfWork.discountRepository.GetByID(productDto.DiscountId.Value);
                product.Discount = discount;  
            }
            product.ImageUrls = imageUrls;
            product.ImagePublicIds = imagePublicIds;
            product.ImageUrlsJson = System.Text.Json.JsonSerializer.Serialize(imageUrls);
            product.ImagePublicIdsJson = System.Text.Json.JsonSerializer.Serialize(imagePublicIds);

            await _unitOfWork.productRepository.CreateAsync(product);
            _unitOfWork.Complete();

            _logger.Log("Product added successfully.", "info");
            return product.Adapt<ProductResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in AddProductAsync: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductRequestDto productDto)
    {
        _logger.Log($"Starting UpdateProductAsync for product ID: {id}.", "info");

        try
        {
            var existingProduct = await _unitOfWork.productRepository.GetByID(id);
            if (existingProduct is null)
            {
                _logger.Log($"Product with ID {id} not found.", "error");
                throw new Exception("Product not found.");
            }

            productDto.Adapt(existingProduct);

            await _unitOfWork.productRepository.UpdateAsync(id, existingProduct);
            _unitOfWork.Complete();

            _logger.Log($"Product with ID {id} updated successfully.", "info");
            return existingProduct.Adapt<ProductResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in UpdateProductAsync: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<string> DeleteProductAsync(int id)
    {
        _logger.Log($"Starting DeleteProductAsync for product ID: {id}.", "info");

        try
        {
            var product = await _unitOfWork.productRepository.GetByID(id);
            if (product is null)
            {
                _logger.Log($"Product with ID {id} not found.", "error");
                throw new Exception("Product not found.");
            }

            await _unitOfWork.productRepository.DeleteAsync(id);
            _unitOfWork.Complete();

            _logger.Log($"Product with ID {id} deleted successfully.", "info");
            return "Product deleted successfully.";
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in DeleteProductAsync: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        _logger.Log($"Fetching product with ID: {id}.", "info");

        try
        {
            var product = await _unitOfWork.productRepository.GetByID(id);
            if (product is null)
            {
                _logger.Log($"Product with ID {id} not found.", "error");
                throw new Exception("Product not found.");
            }

            _logger.Log($"Product with ID {id} retrieved successfully.", "info");
            return product.Adapt<ProductResponseDto>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in GetProductByIdAsync: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        _logger.Log("Fetching all products.", "info");

        try
        {
            var products = await _unitOfWork.productRepository.GetAllAsync();
            _logger.Log("All products retrieved successfully.", "info");
            return products.Adapt<IEnumerable<ProductResponseDto>>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error in GetAllProductsAsync: {ex.Message}", "error");
            throw;
        }
    }
}
