using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class InviteMailDto : IDto
{
    public string Email { get; set; } = null!;
    public string InvitingUser { get; set; } = null!;
    public bool IsRegistered { get; set; }
}