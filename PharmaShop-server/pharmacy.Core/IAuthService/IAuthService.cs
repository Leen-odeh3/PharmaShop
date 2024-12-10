using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.IAuthService;
public interface IAuthService
{
    Task<PharmacistResponseDto> RegisterPharmacistAsync(PharmacistRequestDto pharmacistRequestDto);
    Task<CustomerResponseDto> RegisterCustomerAsync(CustomerRequestDto customerRequestDto);
    Task<AdminResponseDto> RegisterAdminAsync(AdminDto admin);
    Task<AuthModel> Login(LoginDto login);
    Task<string> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequestDto);
    Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto);

}


