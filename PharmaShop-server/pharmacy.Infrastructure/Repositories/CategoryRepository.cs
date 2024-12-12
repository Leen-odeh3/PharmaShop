using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category> ,ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context):base(context)
    {
        
    }

}
