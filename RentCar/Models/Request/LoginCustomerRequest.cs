using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.Models.Request;

public class LoginCustomerRequest
{
    [Required]
    public string email { get; set; }
    [Required]
    public string password { get; set; }
}
