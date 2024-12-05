using AutoMapper;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.Entities;
using pharmacy.Core;

namespace pharmacy.Application.Services;
public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BrandGetResponse> AddBrandAsync(BrandRequestDto brandRequestDto)
    {
        var brand = _mapper.Map<Brand>(brandRequestDto);
        var createdBrand = await _unitOfWork.brandRepository.CreateAsync(brand);
        _unitOfWork.Complete();
        return _mapper.Map<BrandGetResponse>(createdBrand);
    }

    public async Task<BrandGetResponse> UpdateBrandAsync(int id, BrandRequestDto brandRequestDto)
    {
        var brand = _mapper.Map<Brand>(brandRequestDto);
        brand.BrandId = id;

        var updatedBrand = await _unitOfWork.brandRepository.UpdateAsync(id, brand);
        if (updatedBrand == null)
        {
            return null;
        }

        _unitOfWork.Complete();
        return _mapper.Map<BrandGetResponse>(updatedBrand);
    }

    public async Task<string> DeleteBrandAsync(int id)
    {
        var result = await _unitOfWork.brandRepository.DeleteAsync(id);
       _unitOfWork.Complete();
        return "deleted Brand Entity";
    }

    public async Task<BrandGetResponse> GetBrandByIdAsync(int id)
    {
        var brand = await _unitOfWork.brandRepository.GetByID(id);
        return brand is null ? null : _mapper.Map<BrandGetResponse>(brand);
    }

    public async Task<IEnumerable<BrandGetResponse>> GetAllBrandsAsync()
    {
        var brands = await _unitOfWork.brandRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandGetResponse>>(brands);
    }
}