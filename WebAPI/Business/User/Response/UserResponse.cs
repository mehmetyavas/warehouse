using WebAPI.Data.Dto;

namespace WebAPI.Business.User.Response;

public class UserResponse : UserDto
{
    public string? MobilePhones { get; set; }
}