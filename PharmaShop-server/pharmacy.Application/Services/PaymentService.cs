using pharmacy.Core.Entities;
using Microsoft.Extensions.Configuration;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Enums;
using pharmacy.Core.Specifications.OrderSpecifications;
using pharmacy.Core;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Core.Services.Contract;
using Stripe;

namespace pharmacy.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public PaymentService(
        IBasketRepository basketRepo,
        IUnitOfWork unitOfWork,
        IConfiguration configuration
        )
    {
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<Order?> ChangeOrderStatuseAsync(string paymentintent, bool Flag)
    {
        var order = await _unitOfWork.orderRepository.GetByIdAsyncWithSpec(new OrderSpecificationsToGetOrderByPaymentintentId(paymentintent));

        if (Flag == true)
        {
            order.status = OrderStatus.paymentSucceded;
        }
        else
        {
            order.status = OrderStatus.paymentSucceded;
        }

        _unitOfWork.orderRepository.UpdateAsync(order.Id,order);

        _unitOfWork.Complete();

        return order;
    }

    public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string basketid)
    {
        StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

        var basket = await _basketRepo.GetBasketAsync(basketid);

        if (basket is null)
            return null;

        if (basket.Items.Count != 0)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.productRepository.GetByID(item.Id);

                if (product.Price != item.Price)
                    item.Price = product.Price;
            }
        }
        var deliverymethodprice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliverymethod = await _unitOfWork.DeliveryMethodRepo.GetByID(basket.DeliveryMethodId.Value);

            deliverymethodprice = deliverymethod.Cost;
        }

        var ToTalPrice = basket.Items.Sum(i => i.Price * 100 * i.Quantity) + deliverymethodprice * 100;

        PaymentIntentService paymentIntentService = new PaymentIntentService();

        if (string.IsNullOrEmpty(basket.PaymentIntentId))  
        {

            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
            {
                Amount = (long)ToTalPrice,
                PaymentMethodTypes = new List<string>() { "card" },
                Currency = "usd"
            };

            var paymentIntent = await paymentIntentService.CreateAsync(options);

            basket.PaymentIntentId= paymentIntent.Id;
            basket.ClientSecret= paymentIntent.ClientSecret;

            await _basketRepo.UpdateBasketAsync(basket);
        }
        else 
        {
            PaymentIntentUpdateOptions options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)ToTalPrice,
            };

            await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
        }

        return basket;

    }
}
