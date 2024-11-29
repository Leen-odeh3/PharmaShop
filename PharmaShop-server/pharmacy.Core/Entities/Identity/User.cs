using Microsoft.AspNetCore.Identity;
using pharmacy.Core.Enums;
namespace pharmacy.Core.Entities.Identity;
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Roles UserType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
