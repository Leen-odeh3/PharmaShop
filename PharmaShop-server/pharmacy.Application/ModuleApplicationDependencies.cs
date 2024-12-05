using Microsoft.Extensions.DependencyInjection;
using pharmacy.Application.Services;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Contracts.ILogger;
using pharmacy.Core.Contracts.IServices;
using pharmacy.Core.Services;
using pharmacy.Infrastructure.Application.AuthService;
using pharmacy.Infrastructure.Application.Logger;

namespace pharmacy.Application;
public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ILog, Log>();
        services.AddScoped<IAuthService, AuthService>();
        //  services.AddScoped<IUserManager,UserManager>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IProductService,ProductService>();
        services.AddScoped<IReviewService, ReviewService>();  
        services.AddScoped<PaymentMethodService, PaymentMethodService>();
        services.AddScoped<CartService, CartService>();
        services.AddScoped<CartItemService, CartItemService>();


        return services;
    }
}