using System.ComponentModel.DataAnnotations;
using WebAPI.Attributes.CustomAttributes;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;
using WebAPI.Extensions;
using Unique = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace WebAPI.Data.Entity;

//TODO: Profil ekle

[Unique(nameof(Email), IsUnique = true)]
[Unique(nameof(MobilePhones), IsUnique = true)]
public class User : BaseEntity
{
    [MinLength(2), MaxLength(80), Required]
    public string FullName { get; set; } = null!;


    [MaxLength(80), EmailAddress, Required]
    public string Email { get; set; } = null!;

    [PhoneNumber, Required] public string? MobilePhones { get; set; }

    public Gender Gender { get; set; }
    public string? AvatarUrl { get; set; }
    public DateOnly BirthDate { get; set; }


    [Required] public string VerifyToken { get; set; } = null!;

    public bool IsVerified { get; set; }
    public DateTime VerifiedAt { get; set; }
    public string? RefreshToken { get; set; }
    public long LoginCode { get; set; }

    public DateTime LoginCodeExpiredAt { get; set; }


    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();


    public List<string> Roles()
    {
        if (!UserRoles!.Any())
            return new List<string>();

        var roles = UserRoles!.Select(x => ((int)x.RoleId).ToString());

        return roles.ToList();
    }


    public bool IsAdmin()
    {
        if (!UserRoles!.Any())
            return false;

        return UserRoles!.Any(x => x.RoleId == Enum.Roles.Admin);
    }

    public List<string> ActionIds()
    {
        var ActionNames = new List<string>();

        if (!UserRoles.Any())
            return ActionNames;


        var actionClaims = UserRoles.Select(x => x.Role.RolePermissions);
        foreach (var actions in actionClaims)
        {
            foreach (var action in actions)
            {
                ActionNames.Add(action.ActionId.GetValueString());
            }
        }

        return ActionNames;
    }
}