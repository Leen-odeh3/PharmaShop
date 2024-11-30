using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category> ,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context):base(context)
    {
        
    }

}
