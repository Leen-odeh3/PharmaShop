using pharmacy.Core.Contracts;

namespace pharmacy.Core;
public interface IUnitOfWork
{
    ICategoryRepository categoryRepository { get; }
    IProductRepository productRepository { get; }
    IBrandRepository brandRepository { get; }
    IPhotoService photoService { get; }
    IDiscountRepository discountRepository { get; }
    IOrderRepository orderRepository { get; }
    IOrderItemRepository orderItemRepository { get; }
    public ICartRepository cartRepository { get;  }
    public ICartItemRepository cartItemRepository { get; }
    IPaymentMethodRepository PaymentMethodRepository { get; }
    public IReviewRepository reviewRepository { get;  }
    int Complete();
}
