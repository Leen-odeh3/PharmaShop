using Microsoft.Extensions.DependencyInjection;
using pharmacy.Application.Services;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Contracts.ILogger;
using pharmacy.Core.Contracts.IServices;
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
        return services;
    }
}