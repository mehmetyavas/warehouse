using System.ComponentModel.DataAnnotations;
using WebAPI.Attributes;
using WebAPI.Attributes.CustomAttributes;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.Auth.Request;

public class RegisterRequest : IDto
{
    [Required, MinLength(2), MaxLength(20)]
    public string FullName { get; set; } = null!;

    [Required, MinLength(7), MaxLength(40), EmailAddress]
    public string Email { get; set; } = null!;

    [Required, Password] public string Password { get; set; } = null!;

    [Required, PhoneNumber] public string MobilPhones { get; set; } = null!;
}