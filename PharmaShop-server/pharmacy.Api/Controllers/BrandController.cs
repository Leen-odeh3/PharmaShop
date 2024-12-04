using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Brand;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;
    private readonly IResponseHandler _responseHandler;

    public BrandController(IBrandService brandService, IResponseHandler responseHandler)
    {
        _brandService = brandService;
        _responseHandler = responseHandler;
    }

    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] BrandRequestDto brandRequestDto)
    {
        if (brandRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid brand data.");
        }

        var brandResponseDto = await _brandService.AddBrandAsync(brandRequestDto);
        return _responseHandler.Created(brandResponseDto, "Brand created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandRequestDto brandRequestDto)
    {
        if (brandRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid brand data.");
        }

        var updatedBrand = await _brandService.UpdateBrandAsync(id, brandRequestDto);
        if (updatedBrand == null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(updatedBrand, "Brand updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var brand = await _brandService.GetBrandByIdAsync(id);
        if (brand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }
        var result = await _brandService.DeleteBrandAsync(id);
        return  _responseHandler.Success(result, "Brand deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _brandService.GetBrandByIdAsync(id);
        if (brand is null)
        {
            return _responseHandler.NotFound("Brand not found.");
        }

        return _responseHandler.Success(brand, "Brand retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandService.GetAllBrandsAsync();
        return _responseHandler.Success(brands, "Brands retrieved successfully.");
    }
}