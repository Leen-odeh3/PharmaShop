
namespace pharmacy.Core.Services.Contract;
public interface ICachadResponseService
{
    Task CachadResponseAsync(string key, object value, TimeSpan time);

    Task<string?> GetCachadResponseAsync(string key);


}
