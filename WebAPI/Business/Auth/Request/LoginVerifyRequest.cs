using System.ComponentModel.DataAnnotations;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.Auth.Request;

public class LoginVerifyRequest : IDto
{
    [Required] public long LoginCode { get; set; }
}