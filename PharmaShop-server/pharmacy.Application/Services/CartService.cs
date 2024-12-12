using pharmacy.Core;
using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;

namespace pharmacy.Application.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayService _payService;

    public CartService(IUnitOfWork unitOfWork,IPayService payService)
    {
        _unitOfWork = unitOfWork;
        _payService = payService;
    }
    public async Task<ServiceResponse> CheckOut(Checkout checkout)
    {
        var (Products, totalAmount) = await GetTotalAmount(checkout.carts);

        var paymentMethods = await _unitOfWork.PaymentMethodRepository.GetPaymentMethods();
        var isValidPayment = paymentMethods.Any(pm => pm.PaymentMethodId == checkout.PaymentMethodId);

        if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.PaymentMethodId)
          return  await _payService.Pay(totalAmount, Products,checkout.carts);

        return new ServiceResponse(false, "Invalid Payment");
    }


    public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
    {
        var result = await _unitOfWork.cartRepository.SaveCheckoutHistory((IEnumerable<Achieve>)achieves);
        return result > 0 ? new ServiceResponse(true, "Checkout achieved") : new ServiceResponse(false, "Checkout Not achieved");
    }

    private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<Cart> carts)
    {
        if (!carts.Any()) return (Enumerable.Empty<Product>(), 0);

        var products = await _unitOfWork.productRepository.GetAllAsync();
        if (!products.Any()) return (Enumerable.Empty<Product>(), 0);

        var cartProducts = carts
            .Select(cartItem => products.FirstOrDefault(p => p.ProductId == cartItem.ProductId))
            .Where(product => product != null)
            .ToList();

        var totalAmount = carts
            .Where(cartItem => cartProducts.Any(p => p.ProductId == cartItem.ProductId))
            .Sum(cartItem => cartItem.Quantity *
                cartProducts.First(p => p.ProductId == cartItem.ProductId)!.Price);

        return (products, totalAmount);
    }
}
