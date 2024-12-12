using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;
using Stripe.Checkout;

namespace pharmacy.Application.Services;
public class StripePaymentService : IPayService
{
    public StripePaymentService()
    {
        
    }
    public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<Cart> carts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cartProducts)
            {
                var pQuantity = carts.FirstOrDefault(_ => _.ProductId == item.ProductId);
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName,
                            Description = item.ProductDescription
                        },
                        UnitAmount = (long)(item.Price * 100)
                    },
                    Quantity = pQuantity!.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["usd"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https:/localhost:5068/payment-success",
                CancelUrl = "http:/localhost:5068/payment-cancel"
            };

            var service = new SessionService();

            Session session = await service.CreateAsync(options);

            return new ServiceResponse(true, session.Url);
        }
        catch ( Exception ex)
        {
            return new ServiceResponse(false, ex.Message);
        }
    }
}
