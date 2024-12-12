using pharmacy.Api.Extentions;
using pharmacy.Api.Mapping;
using pharmacy.Api.Middlewares;
using pharmacy.Application;
using pharmacy.Infrastructure;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace pharmacy.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddPresentationDependencies(builder.Configuration)
                        .AddInfrastructureDependencies()
                        .AddSwaggerDocumentation()
                        .AddCloudinary(builder.Configuration)
                        .AddCorsPolicy().AddApplicationDependencies();
        builder.Host.UseSerilog();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<GlobalExceptionHandling>();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();  
        app.UseAuthorization();  

        app.MapControllers();

        app.Run();
    }
}
