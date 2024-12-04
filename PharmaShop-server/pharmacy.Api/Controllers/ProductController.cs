using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs.Product;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IResponseHandler _responseHandler;

    public ProductController(IProductService productService, IResponseHandler responseHandler)
    {
        _productService = productService;
        _responseHandler = responseHandler;
    }

    [HttpPost("Addproduct")]
    public async Task<IActionResult> AddProduct([FromForm] ProductRequestDto productDto, [FromForm] List<IFormFile> images)
    {
        try
        {
            var productResponseDto = await _productService.AddProductAsync(productDto, images);
            return _responseHandler.Success(productResponseDto, "Product added successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto productRequestDto)
    {
        try
        {
            var productResponseDto = await _productService.UpdateProductAsync(id, productRequestDto);
            return _responseHandler.Success(productResponseDto, "Product updated successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var message = await _productService.DeleteProductAsync(id);
            return _responseHandler.Success(message, "Product deleted successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var productResponseDto = await _productService.GetProductByIdAsync(id);
            return _responseHandler.Success(productResponseDto, "Product retrieved successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return _responseHandler.Success(products, "Products retrieved successfully.");
    }
}
