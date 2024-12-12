using Microsoft.AspNetCore.Identity;
namespace pharmacy.Core.Entities.Identity;
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserType { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<RefreshToken>? RefreshTokens { get; set; }
}
