using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Contracts;
using pharmacy.Core.Entities;
using pharmacy.Infrastructure.DbContext;

namespace pharmacy.Infrastructure.Repositories;
public class PaymentMethodRepository: GenericRepository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(ApplicationDbContext context):base(context)
    {
        
    }
    public async Task<PaymentMethod> GetPaymentMethodByOrderIdAsync(int orderId)
    {
        return await _context.PaymentMethods
       .FirstOrDefaultAsync(pm => pm.OrderId == orderId);
    }
}
