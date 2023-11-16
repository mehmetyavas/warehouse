using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class MailDto : IDto
{
    [Required, EmailAddress] public string Email { get; init; } = null!;

    [Required] public string Subject { get; set; } = null!;
    [Required] public string Body { get; set; } = null!;
}