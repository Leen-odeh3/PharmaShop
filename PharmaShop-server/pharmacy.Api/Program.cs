using pharmacy.Api.Extentions;
using pharmacy.Api.Mapping;
using pharmacy.Application;
using pharmacy.Infrastructure;
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

        MapsterConfig.Configure();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();  
        app.UseAuthorization();  

        app.MapControllers();

        app.Run();
    }
}
