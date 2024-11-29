using Microsoft.AspNetCore.Identity;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Contracts.ILogger;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.Entities;
using pharmacy.Core.Enums;
using System.Data;

namespace pharmacy.Infrastructure.Application.AuthService;
public class AuthService : IAuthService
{
    private readonly IUserManager _userManager;
    private readonly ITokenService _tokenService;
    private readonly ILog _log;

    public AuthService(IUserManager userManager, ITokenService tokenService, ILog log)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _tokenService = tokenService;
        _log = log;
    }
    public async Task<AdminResponseDto> RegisterAdminAsync(AdminDto registerAdminDto)
    {
        var admin = new Admin
        {
            Email = registerAdminDto.Email,
            UserName = registerAdminDto.Email,
            UserType = Roles.Admin,
            FirstName = registerAdminDto.FirstName,
            LastName = registerAdminDto.LastName
        };

        var result = await _userManager.CreateAsync(admin, registerAdminDto.Password);
        if (result.Succeeded)
        {
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
        var customer = new Pharmacist
        {
            Email = customerRequestDto.Email,
            UserName = customerRequestDto.Email.Split('@')[0],
            UserType = Roles.Pharmacist,
            FirstName = customerRequestDto.FirstName,
            LastName = customerRequestDto.LastName,
        };

        var result = await _userManager.CreateAsync(customer, customerRequestDto.Password);
        if (result.Succeeded)
        {
            return new CustomerResponseDto
            {
                Email = customer.Email,
                Roles = customer.UserType.ToString(),
                FirstName = customerRequestDto.FirstName,
                LastName = customerRequestDto.LastName,
                Address = customerRequestDto.Address,
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to register admin: {errors}");
    }

    public async Task<PharmacistResponseDto> RegisterPharmacistAsync(PharmacistRequestDto pharmacistRequestDto)
    {
        var pharmacist = new Pharmacist
        {
            Email = pharmacistRequestDto.Email,
            UserName = pharmacistRequestDto.Email.Split('@')[0],
            UserType= Roles.Pharmacist,
            FirstName = pharmacistRequestDto.FirstName,
            LastName = pharmacistRequestDto.LastName,
            LicenseNumber= pharmacistRequestDto.LicenseNumber
        };

        var result = await _userManager.CreateAsync(pharmacist, pharmacistRequestDto.Password);
        if (result.Succeeded)
        {
            return new PharmacistResponseDto
            {
                Email = pharmacist.Email,
                Roles = pharmacist.UserType.ToString(),
                FirstName = pharmacistRequestDto.FirstName,
                LastName = pharmacistRequestDto.LastName,
                LicenseNumber=pharmacistRequestDto.LicenseNumber
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception($"Failed to register pharmacist: {errors}");
    }
}
