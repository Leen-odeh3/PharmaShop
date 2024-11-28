using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandRepository _brandRepository; 
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public BrandController(IBrandRepository brandRepository, IResponseHandler responseHandler, IMapper mapper)
    {
        _brandRepository = brandRepository;
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

        var brandResponseDto = await _brandRepository.CreateAsync(brand);
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

        var updatedBrand = await _brandRepository.UpdateAsync(brand);
        if (updatedBrand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(updatedBrand, "Brand updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var result = await _brandRepository.DeleteAsync(id);
        return _responseHandler.Success(result, "Brand deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _brandRepository.GetByID(id);
        if (brand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(brand, "Brand retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandRepository.GetAllAsync();
        return _responseHandler.Success(brands, "Brands retrieved successfully.");
    }
}