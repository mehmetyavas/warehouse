using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.Auth.Request;

public class LoginWithToken : IDto
{
    [Required] public string RefreshToken { get; set; } = null!;
}