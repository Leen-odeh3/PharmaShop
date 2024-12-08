﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pharmacy.Api.Responses;
using pharmacy.Core;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities.Helpers;
using pharmacy.Core.Entities.Identity;
using pharmacy.Infrastructure.Application;
using pharmacy.Infrastructure.DbContext;
using System.Reflection;
using System.Text;

namespace pharmacy.Api.Extentions;
public static class ModulePresentationDependencies
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
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
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
         };
     });

       services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddScoped<IPhotoService, PhotoService>();
        /* services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });*/

        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IResponseHandler, ResponseHandler>();

        return services;
    }
}