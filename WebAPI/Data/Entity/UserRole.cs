using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity;

public class UserRole : BaseEntity
{
    [Required, ForeignKey(nameof(RoleId))] public Roles RoleId { get; set; }
    [Required, ForeignKey(nameof(UserId))] public int UserId { get; set; }


    public virtual Role Role { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}