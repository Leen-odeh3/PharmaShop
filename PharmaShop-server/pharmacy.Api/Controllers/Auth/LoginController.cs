using Microsoft.AspNetCore.Mvc;
using pharmacy.Api.Responses;
using pharmacy.Core.DTOs.Login;
using pharmacy.Core.IAuthService;

namespace pharmacy.Api.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class LoginController : BaseApiController
{

    private readonly ILoginService _authService;
    private readonly IResponseHandler _responseHandler;

    public LoginController(ILoginService authService, IResponseHandler responseHandler)
    {
        _authService = authService;
        _responseHandler = responseHandler;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var authModel = await _authService.Login(loginDto);
        if (authModel.IsAuthenticated)
        {
            return (ActionResult)_responseHandler.Success(authModel, "Login Success");
        }
        return (ActionResult)_responseHandler.Unauthorized(authModel.Message);
    }
}
