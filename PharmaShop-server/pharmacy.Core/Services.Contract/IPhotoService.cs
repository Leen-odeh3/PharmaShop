using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace pharmacy.Core.Services.Contract;
public interface IPhotoService
{
    Task<List<ImageUploadResult>> UploadImagesAsync(IEnumerable<IFormFile> files);
    Task<DeletionResult> DeleteImageAsync(string publicId);
    Task<List<string>> GetImagesByProductIdAsync(int productId);

}
