using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.CustomAttribute;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Product;
using pharmacy.Core.Services.Contract;

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

    /// <summary>
    /// Adds a new product along with its images.
    /// </summary>
    /// <param name="productDto">The product details.</param>
    /// <param name="images">The list of images for the product.</param>
    /// <returns>A response indicating the result of the operation.</returns>
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

    [HttpGet("{id}/images")]
    public async Task<IActionResult> GetProductImages(int id)
    {
        try
        {
            var images = await _productService.GetProductImagesAsync(id);
            if (images == null || images.Count == 0)
                return _responseHandler.NotFound("No images found for this product.");        

            return _responseHandler.Success(images, "Product images retrieved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
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
   // [Cachad(200)]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return _responseHandler.Success(products, "Products retrieved successfully.");
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string name)
    {
        try
        {
            var products = await _productService.SearchProductsByNameAsync(name);
            if (!products.Any())
                return _responseHandler.NotFound("No products found matching the given name.");

            return _responseHandler.Success(products, "Products retrieved successfully.");

        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }

}
