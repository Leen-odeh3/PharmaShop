using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.Application;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public ProductController(IUnitOfWork unitOfWork, IResponseHandler responseHandler, IMapper mapper,IPhotoService photoService)
    {
        _unitOfWork = unitOfWork;
        _responseHandler = responseHandler;
        _mapper = mapper; 
        _photoService = photoService;

    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromForm] ProductRequestDto productDto, [FromForm] List<IFormFile> images)
    {
        var uploadResults = await _photoService.UploadImagesAsync(images);

        if (uploadResults?.Any() == true)
        {
            var imageUrls = new List<string>();
            var imagePublicIds = new List<string>();

            foreach (var result in uploadResults)
            {
                imageUrls.Add(result?.Url?.ToString()); 
                imagePublicIds.Add(result?.PublicId);   
            }

            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                ImageUrls = imageUrls,
                ImagePublicIds = imagePublicIds,
                CategoryId = productDto.CategoryId
            };

            await _unitOfWork.productRepository.CreateAsync(product);
             _unitOfWork.Complete();

            return Ok(new { message = "Product added successfully", product });
        }

        return BadRequest("Failed to upload images.");
    }


    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
    {
        if (productRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var product = _mapper.Map<Product>(productRequestDto);

        var productResponseDto = await _unitOfWork.productRepository.CreateAsync(product);
        _unitOfWork.Complete();
        return _responseHandler.Created(productResponseDto, "Product created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto productRequestDto)
    {
        if (productRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var product = _mapper.Map<Product>(productRequestDto);
        product.ProductId = id;  

        var updatedProduct = await _unitOfWork.productRepository.UpdateAsync(product);
        _unitOfWork.Complete();
        if (updatedProduct is null)
        {
            return _responseHandler.NotFound("Product not found.");
        }

        return _responseHandler.Success(updatedProduct, "Product updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _unitOfWork.productRepository.DeleteAsync(id);
        _unitOfWork.Complete();
        return _responseHandler.Success(result, "Product deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _unitOfWork.productRepository.GetByID(id);
        if (product is null)
        {
            return _responseHandler.NotFound("Product not found.");
        }

        return _responseHandler.Success(product, "Product retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _unitOfWork.productRepository.GetAllAsync();
        return _responseHandler.Success(products, "Products retrieved successfully.");
    }
}
