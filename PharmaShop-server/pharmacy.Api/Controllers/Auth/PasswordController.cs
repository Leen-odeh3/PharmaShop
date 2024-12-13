using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.IAuthService;

namespace pharmacy.Api.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class PasswordController :BaseApiController
{

    private readonly IPasswordService _authService;
    private readonly IResponseHandler _responseHandler;

    public PasswordController(IPasswordService authService, IResponseHandler responseHandler)
    {
        _authService = authService;
        _responseHandler = responseHandler;
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
