using Microsoft.AspNetCore.Identity;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.Contracts.IAuthService;
public interface IUserManager
{
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<User> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(User user, string password);
}