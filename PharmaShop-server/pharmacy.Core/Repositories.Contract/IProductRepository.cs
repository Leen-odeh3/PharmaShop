using pharmacy.Core.Entities;

namespace pharmacy.Core.Repositories.Contract;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product> GetProductImagesAsync(int id);
    Task<IEnumerable<Product>> SearchByNameAsync(string name);

}
