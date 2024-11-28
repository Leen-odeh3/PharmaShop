using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper; 

    public ProductController(IProductRepository productRepository, IResponseHandler responseHandler, IMapper mapper)
    {
        _productRepository = productRepository;
        _responseHandler = responseHandler;
        _mapper = mapper; 
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
