using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.Services.Contract;
using Stripe;

namespace pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        
        [HttpGet("paymet-methods")]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetAll()
        {
            var result = await _paymentMethodService.GetPaymentMethod();
            return Ok(result);
        }

    }
}
