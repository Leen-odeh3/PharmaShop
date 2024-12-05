namespace pharmacy.Core.Contracts;
public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(int id,T entity);
    Task<string> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByID(int id);
}
