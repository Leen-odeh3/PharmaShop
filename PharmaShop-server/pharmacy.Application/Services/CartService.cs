using pharmacy.Core.DTOs.PaymentMethod;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;
using pharmacy.Core;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPayService _payService;

    public CartService(IUnitOfWork unitOfWork, IPayService payService)
    {
        _unitOfWork = unitOfWork;
        _payService = payService;
    }

    public async Task<ServiceResponse> CheckOut(Checkout checkout)
    {
        var (cartProducts, totalAmount) = await GetTotalAmount(checkout.carts);

        if (!cartProducts.Any())
            return new ServiceResponse(false, "No valid products in the cart");

        var paymentMethods = await _unitOfWork.PaymentMethodRepository.GetPaymentMethods();
        var isValidPayment = paymentMethods.Any(pm => pm.PaymentMethodId == checkout.PaymentMethodId);

        if (!isValidPayment)
            return new ServiceResponse(false, "Invalid Payment Method");

        var paymentResponse = await _payService.Pay(totalAmount, cartProducts, checkout.carts);
        return paymentResponse;
    }

    public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
    {
        var achieveEntities = achieves.Select(a => new Achieve
        {
            ProductId = a.ProductId,
            CustomerId = a.CustomerId,
            Quantity = a.Quantity,
            CeatedAt = DateTime.UtcNow
        });

        var result = await _unitOfWork.cartRepository.SaveCheckoutHistory(achieveEntities);
        return result > 0
            ? new ServiceResponse(true, "Checkout history saved successfully")
            : new ServiceResponse(false, "Failed to save checkout history");
    }

    private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<Cart> carts)
    {
        if (!carts.Any()) return (Enumerable.Empty<Product>(), 0);

        var products = await _unitOfWork.productRepository.GetAllAsync();
        var productDictionary = products.ToDictionary(p => p.ProductId);

        var cartProducts = carts
            .Where(c => productDictionary.ContainsKey(c.ProductId))
            .Select(c => productDictionary[c.ProductId])
            .ToList();

        var totalAmount = carts
            .Where(c => productDictionary.ContainsKey(c.ProductId))
            .Sum(c => c.Quantity * productDictionary[c.ProductId].Price);

        return (cartProducts, totalAmount);
    }
}
