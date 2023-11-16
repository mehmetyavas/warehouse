using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity;

public class RolePermission : BaseEntity
{

    [Required,ForeignKey("RoleId")]
    public Roles RoleId { get; set; }
    

    [Required,ForeignKey("ActionId")]
    public ActionName ActionId { get; set; }


    public virtual Role Role { get; set; } = null!;
}