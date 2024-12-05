using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Infrastructure.DbContext;
namespace pharmacy.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }
    public async Task<string> DeleteAsync(int id)
    {
        var model = await _context.Set<T>().FindAsync(id);
        if (model is null)
        {
            return "Entity not found";
        }
        _context.Set<T>().Remove(model);
        return "Entity deleted successfully";
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByID(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(int id, T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id);

        if (existingEntity is null)
            return null;  

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        return existingEntity;
    }
}