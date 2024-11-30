using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace pharmacy.Core.Contracts;
public interface IPhotoService
{
    Task<List<ImageUploadResult>> UploadImagesAsync(IEnumerable<IFormFile> files);
    Task<DeletionResult> DeleteImageAsync(string publicId);

}
