using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Core.Specifications;
using pharmacy.Infrastructure.DbContext;
namespace pharmacy.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
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
        return await _context.Set<T>().AsNoTracking().ToListAsync();
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



    public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecifications<T> specifications)
    {
        return await GenerateQuaryAsync(specifications).AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsyncWithSpec(ISpecifications<T> specifications)
    {
        return await GenerateQuaryAsync(specifications).FirstOrDefaultAsync();
    }

    private IQueryable<T> GenerateQuaryAsync(ISpecifications<T> specifications)
    {
        return SpecificationEvaluator<T>.GetQuary(_context.Set<T>(), specifications);
    }

    public async Task<int> GetCountAsync(ISpecifications<T> specifications)
    {
        return await GenerateQuaryAsync(specifications).CountAsync();
    }

}