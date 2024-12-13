using Microsoft.Extensions.DependencyInjection;
using pharmacy.Application.AuthService;
using pharmacy.Application.Services;
using pharmacy.Core;
using pharmacy.Core.IAuthService;
using pharmacy.Core.ILogger;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Core.Services;
using pharmacy.Core.Services.Contract;
using pharmacy.Infrastructure.Application.AuthService;
using pharmacy.Infrastructure.Application.Logger;
using pharmacy.Infrastructure.Repositories;

namespace pharmacy.Application;
public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ILog, Log>();
        services.AddScoped<IUserRegistrationService,UserRegistrationService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IPasswordService,PasswordService>();

        //  services.AddScoped<IUserManager,UserManager>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IProductService,ProductService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IPayService,StripePaymentService>();
        services.AddScoped<ICartService,CartService>();


        services.AddScoped<Lazy<IUnitOfWork>>(provider => new Lazy<IUnitOfWork>(() => provider.GetRequiredService<IUnitOfWork>()));

        return services;
    }
}