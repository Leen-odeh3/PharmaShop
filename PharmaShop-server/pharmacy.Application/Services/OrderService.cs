
using pharmacy.Core;
using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Services.Contract;
using pharmacy.Core.Entities;
using pharmacy.Core.Specifications.OrderSpecifications;
using pharmacy.Core.Repositories.Contract;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentServices;
    public OrderService(
        IBasketRepository basketRepo,
        IUnitOfWork unitOfWork,
        IPaymentService paymentServices)
    {
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
        _paymentServices = paymentServices;
    }

    public async Task<Order?> CreateOrderAsync(string buyeremail, int deliverymthodid, string basketid, Address address)
    {
        var basket = await _basketRepo.GetBasketAsync(basketid);
        if (basket is not null)
        {
            var OrderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                var productrepo = _unitOfWork.productRepository;
                foreach (var item in basket.Items)
                {
                    var product = await productrepo.GetByID(item.Id);

                    var productitemorder = new ProductItemOrdered(product.Id, product.ProductName);

                    var Orderitem = new OrderItem(productitemorder, item.Quantity, product.Price);

                    OrderItems.Add(Orderitem);
                }
            }

            var subtotal = OrderItems.Sum(O => O.Quantity * O.Price);

            var deliverymethod = await _unitOfWork.DeliveryMethodRepo.GetByID(deliverymthodid);

            var _orderrepo = _unitOfWork.orderRepository;

            var result = await _orderrepo.GetByIdAsyncWithSpec(new OrderSpecificationsToGetOrderByPaymentintentId(basket.PaymentIntentId));

            if (result is not null)
            {
                _orderrepo.DeleteAsync(result.Id);

                await _paymentServices.CreateOrUpdatePaymentIntentAsync(basket.Id);

            }


            var order = new Order(buyeremail, address, deliverymethod.Id , OrderItems, subtotal, basket.PaymentIntentId);

            await _orderrepo.CreateAsync(order);

            var roweffect = _unitOfWork.Complete();
            if (roweffect <= 0)
                return null;

            return order;
        }
        else
        {
            return null;
        }
    }
    public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
         => (IReadOnlyList<DeliveryMethod>)await _unitOfWork.DeliveryMethodRepo.GetAllAsync();
    public async Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string buyerEmail)
    {
       return await _unitOfWork.orderRepository.GetAllAsyncWithSpec(new OrderSpecifications(buyerEmail));
    }
    public async Task<Order?> GetOrderByIdforUserAsync(int orderid, string buyerEmail)
    {
        return await _unitOfWork.orderRepository.GetByIdAsyncWithSpec(new OrderSpecifications(buyerEmail, orderid));
    }
}
