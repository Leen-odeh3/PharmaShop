using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.Contracts.ILogger;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities;

namespace pharmacy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IResponseHandler _responseHandler;
    private readonly ILog _log;
    private readonly IUserManager _userManager;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, IResponseHandler responseHandler, ILog log, IUserManager userManager, ITokenService tokenService)
    {
        _authService = authService;
        _responseHandler = responseHandler;
        _log = log;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    [HttpPost("register/pharmacist")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterPharmacist([FromBody] PharmacistRequestDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterPharmacistAsync(registerDto);
            return _responseHandler.Success(result, "Phamacist registered successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] AdminDto registerAdminDto)
    {
        try
        {
            var result = await _authService.RegisterAdminAsync(registerAdminDto);
            return _responseHandler.Success(result, "Admin registered successfully.");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }


    [HttpPost("register/customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRequestDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterCustomerAsync(registerDto);
            return _responseHandler.Success(result, "Customer registered successfully.");
        }
        catch (DbUpdateException dbEx)
        {
            return _responseHandler.BadRequest($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest(ex.Message);
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return BadRequest("Invalid email or password.");
        }

        var token = _tokenService.GenerateToken(user);
        var response = new LoginResponseDto
        {
            Token = token,
            UserType = user.UserType,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        return _responseHandler.Success(response, "Login successfully");
    }
}