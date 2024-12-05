
using pharmacy.Core.DTOs.Brand;

namespace pharmacy.Core.Contracts.IServices;
public interface IBrandService
{
    Task<BrandGetResponse> AddBrandAsync(BrandRequestDto brandRequestDto);
    Task<BrandGetResponse> UpdateBrandAsync(int id, BrandRequestDto brandRequestDto);
    Task<string> DeleteBrandAsync(int id);
    Task<BrandGetResponse> GetBrandByIdAsync(int id);
    Task<IEnumerable<BrandGetResponse>> GetAllBrandsAsync();
}
