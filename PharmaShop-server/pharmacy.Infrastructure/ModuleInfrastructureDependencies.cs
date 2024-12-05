using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pharmacy.Core.Contracts;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Contracts.ILogger;
using pharmacy.Core.Entities.Identity;
using pharmacy.Infrastructure.DbContext;
using pharmacy.Infrastructure.Repositories;

namespace pharmacy.Infrastructure;
public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IPaymentMethodRepository,PaymentMethodRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IOrderItemRepository,OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();


        services.AddIdentity<User, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        return services;
    }
}