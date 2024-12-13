using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Admin;
using pharmacy.Core.DTOs.Customer;
using pharmacy.Core.DTOs.Pharmacist;
using pharmacy.Core.IAuthService;

namespace pharmacy.Api.Controllers.Auth;
[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IUserRegistrationService _authService;
    private readonly IResponseHandler _responseHandler;

    public RegistrationController(IUserRegistrationService authService, IResponseHandler responseHandler)
    {
        _authService = authService;
        _responseHandler = responseHandler;
    }

    [HttpPost("register-admin")]
    public async Task<ActionResult> RegisterAdmin(AdminDto registerAdminDto)
    {
        var response = await _authService.RegisterAdminAsync(registerAdminDto);
        return (ActionResult)_responseHandler.Success(response, "Complete");
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
}