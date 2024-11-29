using pharmacy.Api.Responses;

namespace pharmacy.Api.Extentions;
public static class ModulePresentationDependencies
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);

        services.AddScoped<IResponseHandler, ResponseHandler>();


        return services;
    }
}