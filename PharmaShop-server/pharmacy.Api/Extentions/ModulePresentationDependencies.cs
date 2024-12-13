using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using pharmacy.Api.Mapping;
using pharmacy.Api.Responses;
using pharmacy.Api.Validation;
using pharmacy.Application.Services;
using pharmacy.Core;
using pharmacy.Core.Entities.Helpers;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.Services.Contract;
using pharmacy.Infrastructure.DbContext;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace pharmacy.Api.Extentions;
public static class ModulePresentationDependencies
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              ValidIssuer = configuration["JWT:Issuer"],
              ValidAudience = configuration["JWT:Audience"],
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
              RoleClaimType = "role"
          };
      });

        MapsterConfig.Configure();
        ProductMappingConfig.Configure();

        Stripe.StripeConfiguration.ApiKey = configuration["stripe:Secretkey"];

        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse("localhost:5068", true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IResponseHandler, ResponseHandler>();


        services.AddControllers()
                .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<RegisterValidation>();
    });
        services.AddScoped<IWishlistService, WishlistService>();
        return services;
    }
}