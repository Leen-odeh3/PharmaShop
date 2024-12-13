using pharmacy.Core.DTOs.Login;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.IAuthService;
public interface ILoginService
{
    Task<AuthModel> Login(LoginDto login);
}