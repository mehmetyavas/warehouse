using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity;

public class Role : BaseEntity<Roles>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Roles Id { get; set; }

    [Required] public string Name { get; set; } = null!;
     public string? Description { get; set; }


    public virtual ICollection<UserRole> RoleClaims { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}