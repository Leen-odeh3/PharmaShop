﻿using pharmacy.Core;
using pharmacy.Core.Contracts;
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
    public IOrderRepository orderRepository { get; private set; }
    public IOrderItemRepository orderItemRepository { get; private set; }
    public IReviewRepository reviewRepository { get; private set; }
    public IWishListRepositry WishlistRepo { get; private set; }
    public IDeliveryRepository DeliveryMethodRepo { get; private set; }
    public IBasketRepository BasketRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context, IPhotoService photoService, ILog logger)
    {
        _context = context;
        _logger = logger;
        this.photoService = photoService;
        productRepository = new ProductRepository(_context);
        categoryRepository = new CategoryRepository(_context);
        brandRepository = new BrandRepository(_context);
        discountRepository= new DiscountRepository(_context);
        orderRepository=new OrderRepository(_context);
        reviewRepository = new ReviewRepository(_context,_logger);
        WishlistRepo = new WishListRepository(_context);

    }
    public int Complete()
    {
        _logger.Log("Saving changes to the database", "info");
        return _context.SaveChanges();
    }
}
