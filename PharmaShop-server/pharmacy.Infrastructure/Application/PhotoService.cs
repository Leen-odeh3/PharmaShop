using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities.Helpers;

namespace pharmacy.Infrastructure.Application;
public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        var cloudinary = cloudinarySettings.Value;
        if (cloudinary == null || string.IsNullOrEmpty(cloudinary.CloudName) || string.IsNullOrEmpty(cloudinary.ApiKey) || string.IsNullOrEmpty(cloudinary.ApiSecret))
        {
            throw new InvalidOperationException("Cloudinary settings are not properly configured.");
        }

        _cloudinary = new Cloudinary(new Account(cloudinary.CloudName, cloudinary.ApiKey, cloudinary.ApiSecret));

        if (_cloudinary == null)
        {
            throw new InvalidOperationException("Cloudinary instance could not be initialized.");
        }
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
