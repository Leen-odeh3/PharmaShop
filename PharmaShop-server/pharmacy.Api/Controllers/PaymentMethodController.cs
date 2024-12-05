using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.DTOs;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPaymentMethod([FromBody] PaymentMethodRequestDto requestDto)
    {
        var paymentMethod = await _paymentMethodService.AddPaymentMethodAsync(requestDto);
        if (paymentMethod is null)
            return BadRequest("Failed to add payment method");

        return CreatedAtAction(nameof(GetPaymentMethodByOrderId), new { orderId = paymentMethod.OrderId }, paymentMethod);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPaymentMethods()
    {
        var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
        return Ok(paymentMethods);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetPaymentMethodByOrderId(int orderId)
    {
        var paymentMethod = await _paymentMethodService.GetPaymentMethodByOrderIdAsync(orderId);
        if (paymentMethod is null)
            return NotFound("Payment method not found for the given order");

        return Ok(paymentMethod);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaymentMethod(int id)
    {
        var result = await _paymentMethodService.DeletePaymentMethodAsync(id);
        if (result is "NotFound entity")
            return NotFound("Payment method not found");

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaymentMethod(int id, [FromBody] PaymentMethodRequestDto requestDto)
    {
        var updatedPaymentMethod = await _paymentMethodService.UpdatePaymentMethodAsync(id, requestDto);
        if (updatedPaymentMethod == null)
            return NotFound("Payment method not found for the given ID");

        return Ok(updatedPaymentMethod);
    }
}
