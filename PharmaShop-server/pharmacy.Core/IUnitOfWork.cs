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
    public IReviewRepository reviewRepository { get;  }
    IWishListRepositry WishlistRepo { get; }
    ICartRepository cartRepository { get; }

    IPaymentMethodRepository PaymentMethodRepository { get; }
    int Complete();
}
