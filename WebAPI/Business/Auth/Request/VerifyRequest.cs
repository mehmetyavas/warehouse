using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.Auth.Request;

public class VerifyRequest : IDto
{
    [Required] public string Token { get; set; } = null!;
}