using pharmacy.Core.Contracts;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Core.Services.Contract;

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
    public IReviewRepository reviewRepository { get;  }
    IWishListRepositry WishlistRepo { get; }
    IDeliveryRepository DeliveryMethodRepo { get;}
    IBasketRepository BasketRepository { get;}
    int Complete();
}
