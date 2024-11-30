﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.Application;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public ProductController(IProductRepository productRepository, IResponseHandler responseHandler, IMapper mapper,IPhotoService photoService)
    {
        _productRepository = productRepository;
        _responseHandler = responseHandler;
        _mapper = mapper; 
        _photoService = photoService;

    }
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct([FromForm] ProductRequestDto productDto, IFormFile image)
    {
        var uploadResult = await _photoService.UploadImageAsync(image);

        if (uploadResult?.Url != null)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                ImageUrl = uploadResult?.Url?.ToString(),  
                ImagePublicId = uploadResult?.PublicId,  
                CategoryId = productDto.CategoryId
            };

           _productRepository.CreateAsync(product);

            return Ok(new { message = "Product added successfully", product });
        }

        return BadRequest("Failed to upload image");
    }
    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteImageProduct(int id)
    {
        var product = await _productRepository.GetByID(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        var deleteResult = await _photoService.DeleteImageAsync(product.ImagePublicId);
        return BadRequest("Failed to delete image from Cloudinary");
    }


    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
    {
        if (productRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid product data.");
        }

        var product = _mapper.Map<Product>(productRequestDto);

        var productResponseDto = await _productRepository.CreateAsync(product);
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

        var updatedProduct = await _productRepository.UpdateAsync(product);
        if (updatedProduct is null)
        {
            return _responseHandler.NotFound("Product not found.");
        }

        return _responseHandler.Success(updatedProduct, "Product updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productRepository.DeleteAsync(id);
        return _responseHandler.Success(result, "Product deleted successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productRepository.GetByID(id);
        if (product is null)
        {
            return _responseHandler.NotFound("Product not found.");
        }

        return _responseHandler.Success(product, "Product retrieved successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        return _responseHandler.Success(products, "Products retrieved successfully.");
    }
}
