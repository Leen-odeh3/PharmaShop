﻿
namespace pharmacy.Core.DTOs.Customer;
public class CustomerRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}
