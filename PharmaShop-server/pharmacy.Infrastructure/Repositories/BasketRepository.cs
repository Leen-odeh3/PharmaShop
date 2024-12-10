using pharmacy.Core.Entities.OrderAggregate;
using pharmacy.Core.Repositories.Contract;
using StackExchange.Redis;
using System.Text.Json;

namespace pharmacy.Infrastructure.Repositories;
public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public async Task<CustomerBasket?> GetBasketAsync(string id)
    {
        var basket = await _database.StringGetAsync(id);

        if (basket.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<CustomerBasket>(basket);
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        var result = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        if (!result)
            return null;

        return basket;
    }
}
