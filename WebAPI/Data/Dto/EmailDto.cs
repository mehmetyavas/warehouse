using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class EmailDto : IDto
{
    [Required, EmailAddress] public string Email { get; init; } = null!;

    [Required] public string Subject { get; set; } = null!;
    [Required] public string Body { get; set; } = null!;
}

public class EmailNotificationParameters
{
    public string[] SubjectParameters { get; set; } = Array.Empty<string>();
    public string[] BodyParameters { get; set; } = Array.Empty<string>();
    public string[] ActionTextParameters { get; set; } = Array.Empty<string>();
    public string[] ActionRedirectParameters { get; set; } = Array.Empty<string>();
}