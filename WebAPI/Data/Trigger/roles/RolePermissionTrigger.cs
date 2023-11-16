using WebAPI.Data.Entity;
using WebAPI.Data.Trigger.Base;

namespace WebAPI.Data.Trigger.roles;

public class RolePermissionTrigger:BaseTrigger<RolePermission>
{
    public RolePermissionTrigger(AppDbContext context) : base(context)
    {
    }
}