using pharmacy.Core.Entities;
using pharmacy.Core.Specifications;

namespace pharmacy.Core.Repositories.Contract;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(int id, T entity);
    Task<string> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByID(int id);

    public Task<T?> GetByIdAsyncWithSpec(ISpecifications<T> specifications);

    public Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifications<T> specifications);

    public Task<int> GetCountAsync(ISpecifications<T> specifications);
}
