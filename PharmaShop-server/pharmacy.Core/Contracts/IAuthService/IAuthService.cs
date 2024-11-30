using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.Contracts.IAuthService;
public interface IAuthService
{
    Task<PharmacistResponseDto> RegisterPharmacistAsync(PharmacistRequestDto pharmacistRequestDto);
    Task<CustomerResponseDto> RegisterCustomerAsync(CustomerRequestDto customerRequestDto);
    Task<AdminResponseDto> RegisterAdminAsync(AdminDto admin);
    Task<AuthModel> Login(LoginDto login);
}


