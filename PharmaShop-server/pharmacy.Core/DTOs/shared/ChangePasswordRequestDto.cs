﻿
namespace pharmacy.Core.DTOs.shared;
public class ChangePasswordRequestDto
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
