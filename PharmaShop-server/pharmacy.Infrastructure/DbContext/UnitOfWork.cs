using pharmacy.Core;
using pharmacy.Core.ILogger;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Core.Services.Contract;
using pharmacy.Infrastructure.Repositories;

namespace pharmacy.Infrastructure.DbContext;
public class UnitOfWork : IUnitOfWork
{
    private readonly ILog _logger;

    private readonly ApplicationDbContext _context;
    public ICategoryRepository categoryRepository { get; private set; }
    public IProductRepository productRepository { get; private set; }
    public IBrandRepository brandRepository { get; private set; }
    public IPhotoService photoService { get; private set; }
    public IDiscountRepository discountRepository { get; private set; }
    public IReviewRepository reviewRepository { get; private set; }
    public IWishListRepositry WishlistRepo { get; private set; }
    public IPaymentMethodRepository PaymentMethodRepository { get; private set; }
    public ICartRepository cartRepository { get; private set; } 

    public UnitOfWork(ApplicationDbContext context, IPhotoService photoService, ILog logger)
    {
        _context = context;
        _logger = logger;
        this.photoService = photoService;
        productRepository = new ProductRepository(_context);
        categoryRepository = new CategoryRepository(_context);
        brandRepository = new BrandRepository(_context);
        discountRepository= new DiscountRepository(_context);
        reviewRepository = new ReviewRepository(_context,_logger);
        WishlistRepo = new WishListRepository(_context);
        discountRepository = new DiscountRepository(_context);
        PaymentMethodRepository = new PaymentMethodRepository(_context);
        cartRepository = new CartRepository(_context);
    }
    public int Complete()
    {
        _logger.Log("Saving changes to the database", "info");
        return _context.SaveChanges();
    }
}
