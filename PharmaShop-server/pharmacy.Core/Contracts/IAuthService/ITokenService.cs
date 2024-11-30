using pharmacy.Core.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace pharmacy.Core.Contracts.IAuthService;
public interface ITokenService
{
    Task<JwtSecurityToken> CreateJwtToken(User user);
    Task<AuthModel> RefreshTokenAsync(string token);
    RefreshToken GenerateRefreshToken();
    void SetRefreshTokenInCookie(string refreshToken, DateTime expires);
}