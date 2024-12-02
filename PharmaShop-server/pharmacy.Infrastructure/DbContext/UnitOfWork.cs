﻿using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Infrastructure.Repositories;

namespace pharmacy.Infrastructure.DbContext;
public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _context;
    public ICategoryRepository categoryRepository { get; private set; }

    public IProductRepository productRepository { get; private set; }

    public IBrandRepository brandRepository { get; private set; }

    public IPhotoService photoService { get; private set; }

    public IDiscountRepository discountRepository { get; private set; }
    public IOrderRepository orderRepository { get; private set; }
    public IOrderItemRepository orderItemRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context, IPhotoService photoService)
    {
        _context = context;
        this.photoService = photoService;
        productRepository = new ProductRepository(_context);
        categoryRepository = new CategoryRepository(_context);
        brandRepository = new BrandRepository(_context);
        discountRepository= new DiscountRepository(_context);
        orderRepository=new OrderRepository(_context);  
    }
    public int Complete()
    {
        return _context.SaveChanges();
    }
}
