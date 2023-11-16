using WebAPI.Attributes.CustomAttributes;
using WebAPI.Data.Entity.Base;

namespace WebAPI.Business.User.Request;

public class UpdateUserRequest : IDto
{
    public string? FullName { get; set; }
    [PhoneNumber] public string? MobilePhones { get; set; }
    public int Id { get; set; }
}