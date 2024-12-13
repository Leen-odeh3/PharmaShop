using Microsoft.AspNetCore.Identity;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.Enums;
using pharmacy.Core.Entities;
using pharmacy.Core.ILogger;
using pharmacy.Core.IAuthService;

namespace pharmacy.Infrastructure.Application.AuthService;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly UserManager<User> _userManager; 
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILog _log;
    public UserRegistrationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILog log)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager;
        _log = log;
    }

    public async Task<AdminResponseDto> RegisterAdminAsync(AdminDto registerAdminDto)
    {
        _log.Log("Starting Admin registration process.", "info");

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
            _log.Log("Admin role created successfully.", "info");
        }

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            _log.Log($"Admin user {admin.Email} registered successfully.", "info");

            return new AdminResponseDto
            {
                Id = admin.Id,
                Email = admin.Email,
                UserType = admin.UserType.ToString(),
                CreatedAt = DateTime.UtcNow
            };
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        _log.Log($"Failed to register admin: {errors}", "error");
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

}
