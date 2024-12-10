using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.Contracts.IAuthService;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.DTOs.shared;

namespace pharmacy.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IResponseHandler _responseHandler;

    public AuthController(IAuthService authService,IResponseHandler responseHandler)
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
            return (ActionResult)_responseHandler.Success(authModel,"Login Success");
        }
        return (ActionResult)_responseHandler.Unauthorized(authModel.Message);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
    {
        if (changePasswordRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid request data.");
        }

            var result = await _authService.ChangePasswordAsync(changePasswordRequestDto);
            return Ok(new { Message = result });       
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        if (forgotPasswordRequestDto is null)
        {
            return _responseHandler.BadRequest("Invalid request data.");
        }
            var result = await _authService.ForgotPasswordAsync(forgotPasswordRequestDto);
            return Ok(new { Message = result });
      
    }

}