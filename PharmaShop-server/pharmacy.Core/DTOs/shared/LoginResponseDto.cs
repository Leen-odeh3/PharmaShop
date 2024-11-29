
using pharmacy.Core.Enums;

namespace pharmacy.Core.DTOs.shared;
public class LoginResponseDto
{
    public string Token { get; set; }
    public Roles UserType { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}