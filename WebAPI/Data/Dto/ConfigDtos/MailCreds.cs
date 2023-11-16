using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto.ConfigDtos;

public class MailCreds : IDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
}