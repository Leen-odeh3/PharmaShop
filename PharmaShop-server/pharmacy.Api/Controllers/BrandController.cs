using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork; 
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public BrandController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] BrandRequestDto brandRequestDto)
    {
        if (brandRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid brand data.");
        }

        var brand = _mapper.Map<Brand>(brandRequestDto); 

        var brandResponseDto = await _unitOfWork.brandRepository.CreateAsync(brand);
        _unitOfWork.Complete();
        return _responseHandler.Created(brandResponseDto, "Brand created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandRequestDto brandRequestDto)
    {
        if (brandRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid brand data.");
        }

        var brand = _mapper.Map<Brand>(brandRequestDto);  

        var updatedBrand = await _unitOfWork.brandRepository.UpdateAsync(brand);
        _unitOfWork.Complete();
        if (updatedBrand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(updatedBrand, "Brand updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var result = await _unitOfWork.brandRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return _responseHandler.Success(result, "Brand deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _unitOfWork.brandRepository.GetByID(id);
        if (brand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(brand, "Brand retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _unitOfWork.brandRepository.GetAllAsync();
        return _responseHandler.Success(brands, "Brands retrieved successfully.");
    }
}