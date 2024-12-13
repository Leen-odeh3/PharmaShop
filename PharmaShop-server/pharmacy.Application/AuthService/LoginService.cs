using Microsoft.AspNetCore.Identity;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.IAuthService;
using pharmacy.Core.ILogger;
using System.IdentityModel.Tokens.Jwt;

namespace pharmacy.Application.AuthService;
public class LoginService : ILoginService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ILog _log;

    public LoginService(UserManager<User> userManager, ITokenService tokenService, ILog log)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _tokenService = tokenService;
        _log = log;
    }

    public async Task<AuthModel> Login(LoginDto login)
    {
        _log.Log($"Attempting login for email: {login.Email}", "info");

        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
            _log.Log($"Login failed for email: {login.Email}. Invalid credentials.", "warning");
            return new AuthModel
            {
                IsAuthenticated = false,
                Message = "Invalid credentials"
            };
        }

        var jwtToken = await _tokenService.CreateJwtToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshTokens.Add(refreshToken);
        user.LastLogin = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        _log.Log($"Login successful for email: {login.Email}. Token issued.", "info");

        return new AuthModel
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiration = refreshToken.ExpiresOn,
            Username = user.UserName,
            Email = user.Email,
            Roles = (List<string>)await _userManager.GetRolesAsync(user),
            ExpiresOn = jwtToken.ValidTo
        };
    }
}
