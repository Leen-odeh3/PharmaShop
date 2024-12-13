using pharmacy.Core;
using pharmacy.Core.Entities;
using pharmacy.Core.Services.Contract;

namespace pharmacy.Application.Services;
public class PaymentMethodService : IPaymentMethodService
{
    private readonly IUnitOfWork _unitOfWork;
    public PaymentMethodService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethod()
    {
        var methods = await _unitOfWork.PaymentMethodRepository.GetPaymentMethods();

        if(!methods.Any()) {
            throw new KeyNotFoundException("NotFound any paymentMethod");
        }
        return methods;
    }
}
