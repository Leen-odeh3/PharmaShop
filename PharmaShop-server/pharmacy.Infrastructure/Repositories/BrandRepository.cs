using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class BrandRepository :GenericRepository<Brand> ,IBrandRepository
{
    public BrandRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
