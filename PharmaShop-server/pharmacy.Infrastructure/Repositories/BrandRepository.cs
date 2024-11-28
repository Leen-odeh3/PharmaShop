
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class BrandRepository :GenericRepository<Brand> ,IBrandRepository
{
    public BrandRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
