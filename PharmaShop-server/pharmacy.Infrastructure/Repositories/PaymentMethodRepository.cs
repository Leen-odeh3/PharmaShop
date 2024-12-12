
using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Repositories.Contract;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(ApplicationDbContext context):base(context)
    {
        
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
    {
        return await _context.paymentMethods.AsNoTracking().ToListAsync();
    }
}
