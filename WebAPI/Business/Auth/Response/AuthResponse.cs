using WebAPI.Data.Dto;
using WebAPI.Data.Entity.Base;
using WebAPI.Utilities.Security.Jwt;

namespace WebAPI.Business.Auth.Response;

public class AuthResponse : IDto
{
    public AccessToken AccessToken { get; set; } = null!;
     public UserDto User { get; set; } = null!;
}