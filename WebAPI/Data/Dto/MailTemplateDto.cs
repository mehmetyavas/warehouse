using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class MailTemplateDto : IDto
{
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ActionText { get; set; } = null!;
    public string ActionRedirect { get; set; } = null!;
    public string Email { get; set; } = null!;
}