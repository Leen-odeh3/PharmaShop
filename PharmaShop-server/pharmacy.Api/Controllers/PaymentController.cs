using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Services.Contract;
using Stripe;

namespace pharmacy.Api.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentServices;

    public PaymentController(IPaymentService paymentServices)
    {
        _paymentServices = paymentServices;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("{basketid}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePayemntIntent(string basketid)
    {
        var result = await _paymentServices.CreateOrUpdatePaymentIntentAsync(basketid);
        return Ok(result);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebHook()
    {
        const string endpointSecret = "whsec_cc1950828342baa5547942e4a065d8b01caad4241bffa96b984cc1bdf8edeec5";

        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], endpointSecret);

        var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

        if (stripeEvent.Type == "payment_intent.payment_failed")
        {
            await _paymentServices.ChangeOrderStatuseAsync(paymentIntent.Id, true);
        }
        else if (stripeEvent.Type == "payment_intent.succeeded")
        {
            await _paymentServices.ChangeOrderStatuseAsync(paymentIntent.Id, false);
        }

        return Ok();
    }
}
