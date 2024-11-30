using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.DTOs.Pharmacist;


namespace pharmacy.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IResponseHandler _responseHandler;

    public AuthController(IAuthService authService,ResponseHandler responseHandler)
    {
        _authService = authService;
        _responseHandler = responseHandler;
    }

    [HttpPost("register-admin")]
    public async Task<ActionResult> RegisterAdmin(AdminDto registerAdminDto)
    {
        var response = await _authService.RegisterAdminAsync(registerAdminDto);
        return (ActionResult)_responseHandler.Success(response,"Complete");
    }

    [HttpPost("register-customer")]
    public async Task<ActionResult> RegisterCustomer(CustomerRequestDto customerRequestDto)
    {
        var response = await _authService.RegisterCustomerAsync(customerRequestDto);
        return (ActionResult)_responseHandler.Success(response, "Complete");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register-pharmacist")]
    public async Task<ActionResult> RegisterPharmacist(PharmacistRequestDto pharmacistRequestDto)
    {
        var response = await _authService.RegisterPharmacistAsync(pharmacistRequestDto);
        return (ActionResult)_responseHandler.Success(response, "Complete");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var authModel = await _authService.Login(loginDto);
        if (authModel.IsAuthenticated)
        {
            return Ok(authModel);
        }
        return (ActionResult)_responseHandler.Unauthorized(authModel.Message);
    }
}