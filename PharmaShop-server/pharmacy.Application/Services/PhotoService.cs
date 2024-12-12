using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using pharmacy.Core.Entities.Helpers;
using pharmacy.Core.Services.Contract;
using pharmacy.Core;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    private readonly Lazy<IUnitOfWork> _unitOfWork;

    public PhotoService(IOptions<CloudinarySettings> cloudinarySettings, Lazy<IUnitOfWork> unitOfWork)
    {
        var cloudinary = cloudinarySettings.Value;
        if (cloudinary is null || string.IsNullOrEmpty(cloudinary.CloudName) || string.IsNullOrEmpty(cloudinary.ApiKey) || string.IsNullOrEmpty(cloudinary.ApiSecret))
        {
            throw new InvalidOperationException("Cloudinary settings are not properly configured.");
        }

        _cloudinary = new Cloudinary(new Account(cloudinary.CloudName, cloudinary.ApiKey, cloudinary.ApiSecret));

        if (_cloudinary is null)
        {
            throw new InvalidOperationException("Cloudinary instance could not be initialized.");
        }

        _unitOfWork = unitOfWork;
    }

    public async Task<List<string>> GetImagesByProductIdAsync(int productId)
    {
        var unitOfWork = _unitOfWork.Value;
        var product = await unitOfWork.productRepository.GetByID(productId);

        if (product is null)
            throw new Exception("Product not found.");

        return product.ImageUrls ?? new List<string>();
    }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }

    public async Task<List<ImageUploadResult>> UploadImagesAsync(IEnumerable<IFormFile> files)
    {
        var uploadResults = new List<ImageUploadResult>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(uploadResult);
            }
        }

        return uploadResults;
    }
}
