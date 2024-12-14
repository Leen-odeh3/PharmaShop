
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using pharmacy.Core.Services.Contract;
using System.Text;

namespace pharmacy.Api.CustomAttribute;
public class CachadAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeinRedis;

    public CachadAttribute(int TimeinRedis)
    {
        _timeinRedis = TimeinRedis;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var CachadResponseService = context.HttpContext.RequestServices.GetRequiredService<ICachadResponseService>();

        var Key = GenerateCachadResponseKey(context.HttpContext);

        var Response = await CachadResponseService.GetCachadResponseAsync(Key);

        if (!string.IsNullOrEmpty(Response))
        {
            context.Result = new ContentResult
            {
                Content = Response,
                StatusCode = 200,
                ContentType = "application/json"
            };
            return;
        }

        var ActionExecutedContext = await next.Invoke();

        if (ActionExecutedContext.Result is OkObjectResult okObjectResult && okObjectResult is not null)
        {
            await CachadResponseService.CachadResponseAsync(Key, okObjectResult.Value, TimeSpan.FromSeconds(_timeinRedis));
        }


    }


    private string GenerateCachadResponseKey(HttpContext httpContext)
    {
        var QuaryString = httpContext.Request.Query.OrderBy(q => q.Key);

        StringBuilder key = new StringBuilder();

        key.Append(httpContext.Request.Path);

        foreach (var item in QuaryString)
        {
            key.Append($"/{item.Key}={item.Value}");
        }
        return key.ToString();
    }
}