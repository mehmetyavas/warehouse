using System.ComponentModel.DataAnnotations;
using WebAPI.Attributes;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.Auth.Request;

public class LoginRequest : IDto
{

    [Required, EmailAddress] public string Email { get; set; } = null!;
    [Required, Password] public string Password { get; set; } = null!;

    [Required, Password, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}