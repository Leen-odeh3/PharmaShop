using Microsoft.AspNetCore.Identity;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.Enums;
using pharmacy.Core.Contracts.ILogger;
using System.IdentityModel.Tokens.Jwt;
using pharmacy.Core.Entities;
using pharmacy.Core.DTOs.shared;

namespace pharmacy.Infrastructure.Application.AuthService;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager; 
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly ILog _log;

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, ILog log)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager;
        _tokenService = tokenService;
        _log = log;
    }

    public async Task<AdminResponseDto> RegisterAdminAsync(AdminDto registerAdminDto)
    {
        var admin = new User
        {
            Email = registerAdminDto.Email,
            UserName = registerAdminDto.Email,
            FirstName = registerAdminDto.FirstName,
            LastName = registerAdminDto.LastName,
            UserType = Roles.Admin.ToString()  
        };

        var result = await _userManager.CreateAsync(admin, registerAdminDto.Password);

        if (!await _roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        }

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString()); 

            return new AdminResponseDto
            {
                Id = admin.Id,
                Email = admin.Email,
                UserType = admin.UserType.ToString(),
                CreatedAt = DateTime.UtcNow
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to register admin: {errors}");
    }

    public async Task<CustomerResponseDto> RegisterCustomerAsync(CustomerRequestDto customerRequestDto)
    {
        var customer = new User
        {
            Email = customerRequestDto.Email,
            UserName = customerRequestDto.Email.Split('@')[0],
            FirstName = customerRequestDto.FirstName,
            LastName = customerRequestDto.LastName,
            UserType = Roles.Customer.ToString()  
        };

        var result = await _userManager.CreateAsync(customer, customerRequestDto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(customer, Roles.Customer.ToString()); 

            return new CustomerResponseDto
            {
                Email = customer.Email,
                Roles = Roles.Customer.ToString(),
                FirstName = customerRequestDto.FirstName,
                LastName = customerRequestDto.LastName,
                Address = customerRequestDto.Address
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to register customer: {errors}");
    }

    public async Task<PharmacistResponseDto> RegisterPharmacistAsync(PharmacistRequestDto pharmacistRequestDto)
    {
        var pharmacist = new Pharmacist
        {
            Email = pharmacistRequestDto.Email,
            UserName = pharmacistRequestDto.Email.Split('@')[0],
            FirstName = pharmacistRequestDto.FirstName,
            LastName = pharmacistRequestDto.LastName,
            LicenseNumber = pharmacistRequestDto.LicenseNumber,
            UserType = Roles.Pharmacist.ToString()  
        };

        var result = await _userManager.CreateAsync(pharmacist, pharmacistRequestDto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(pharmacist, Roles.Pharmacist.ToString()); 

            return new PharmacistResponseDto
            {
                Email = pharmacist.Email,
                Roles = Roles.Pharmacist.ToString(),
                FirstName = pharmacistRequestDto.FirstName,
                LastName = pharmacistRequestDto.LastName,
                LicenseNumber = pharmacistRequestDto.LicenseNumber
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to register pharmacist: {errors}");
    }

    public async Task<AuthModel> Login(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
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

        return new AuthModel
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiration = refreshToken.ExpiresOn,
            Username = user.UserName,
            Email = user.Email,
            Roles = (List<string>)await _userManager.GetRolesAsync(user),
            ExpiresOn= jwtToken.ValidTo
    };
    }

    public async Task<string> ChangePasswordAsync(ChangePasswordRequestDto changePasswordRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(changePasswordRequestDto.Email);
        if (user is null)
        {
            throw new Exception("User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordRequestDto.OldPassword, changePasswordRequestDto.NewPassword);
        if (result.Succeeded)
        {
            return "Password changed successfully.";
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to change password: {errors}");
        }
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
        if (user is null)
        {
            throw new Exception("User not found.");
        }

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to remove old password: {errors}");
        }

        var passwordChangeResult = await _userManager.AddPasswordAsync(user, forgotPasswordRequestDto.NewPassword);
        if (passwordChangeResult.Succeeded)
        {
            return "Password reset successfully.";
        }
        else
        {
            var errors = string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to reset password: {errors}");
        }
    }

}
