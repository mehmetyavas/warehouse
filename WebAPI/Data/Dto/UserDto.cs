using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class UserDto : IDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string? AvatarUrl { get; set; }
}