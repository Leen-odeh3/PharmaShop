using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.Entities;
using pharmacy.Core;
using pharmacy.Core.Services.Contract;
using Mapster;
using pharmacy.Core.ILogger;

namespace pharmacy.Application.Services;
public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILog _logger;

    public BrandService(IUnitOfWork unitOfWork, ILog logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BrandGetResponse> AddBrandAsync(BrandRequestDto brandRequestDto)
    {
        try
        {
            var brand = brandRequestDto.Adapt<Brand>();
            var createdBrand = await _unitOfWork.brandRepository.CreateAsync(brand);
            _unitOfWork.Complete();
            _logger.Log("Brand added successfully", "info");
            return createdBrand.Adapt<BrandGetResponse>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while adding brand: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<BrandGetResponse> UpdateBrandAsync(int id, BrandRequestDto brandRequestDto)
    {
        try
        {
            var brand = brandRequestDto.Adapt<Brand>();
            brand.Id = id;
            var updatedBrand = await _unitOfWork.brandRepository.UpdateAsync(id, brand);
            if (updatedBrand is null)
            {
                _logger.Log("Brand not found", "warning");
                return null;
            }

            _unitOfWork.Complete();
            _logger.Log("Brand updated successfully", "info");
            return updatedBrand.Adapt<BrandGetResponse>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while updating brand: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<string> DeleteBrandAsync(int id)
    {
        try
        {
            var result = await _unitOfWork.brandRepository.DeleteAsync(id);
            _unitOfWork.Complete();
            _logger.Log("Brand deleted successfully", "info");
            return "deleted Brand Entity";
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while deleting brand: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<BrandGetResponse> GetBrandByIdAsync(int id)
    {
        try
        {
            var brand = await _unitOfWork.brandRepository.GetByID(id);
            if (brand == null)
            {
                _logger.Log("Brand not found", "warning");
                return null;
            }

            _logger.Log("Brand retrieved successfully", "info");
            return brand.Adapt<BrandGetResponse>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while retrieving brand: {ex.Message}", "error");
            throw;
        }
    }

    public async Task<IEnumerable<BrandGetResponse>> GetAllBrandsAsync()
    {
        try
        {
            var brands = await _unitOfWork.brandRepository.GetAllAsync();
            _logger.Log("Brands retrieved successfully", "info");
            return brands.Adapt<IEnumerable<BrandGetResponse>>();
        }
        catch (Exception ex)
        {
            _logger.Log($"Error while retrieving all brands: {ex.Message}", "error");
            throw;
        }
    }
}
