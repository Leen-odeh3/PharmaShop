using pharmacy.Core.Entities.Identity;

namespace pharmacy.Core.Contracts.IAuthService;
public interface ITokenService
{
    string GenerateToken(User user);
}