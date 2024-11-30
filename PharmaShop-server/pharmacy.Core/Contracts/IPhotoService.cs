using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace pharmacy.Core.Contracts;
public interface IPhotoService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    Task<DeletionResult> DeleteImageAsync(string publicId);

}
