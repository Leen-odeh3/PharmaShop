using pharmacy.Core.DTOs.shared;

namespace pharmacy.Core.IAuthService;
public interface IPasswordService
{
    Task<string> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequestDto);
    Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto);
}
