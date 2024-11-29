using Microsoft.AspNetCore.Identity;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Infrastructure.Application.AuthService;
public class UserManager : IUserManager
{
    private readonly UserManager<User> _userManager;

    public UserManager(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
        return await _userManager.CreateAsync(user);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }
}