using Microsoft.AspNetCore.Identity;
using pharmacy.Core.DTOs.shared;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.Exceptions;
using pharmacy.Core.IAuthService;
using pharmacy.Core.ILogger;

namespace pharmacy.Application.AuthService;
public class PasswordService :IPasswordService
{
    private readonly UserManager<User> _userManager;
    private readonly ILog _log;

    public PasswordService(UserManager<User> userManager, ILog log)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _log = log;
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
            return "Password changed successfully.";
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
            throw new NotFoundException("User not found.");

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to remove old password: {errors}");
        }

        var passwordChangeResult = await _userManager.AddPasswordAsync(user, forgotPasswordRequestDto.NewPassword);
        if (passwordChangeResult.Succeeded)
            return "Password reset successfully.";
        else
        {
            var errors = string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to reset password: {errors}");
        }
    }
}
