using WebAPI.Data.Entity;
using WebAPI.Data.Trigger.Base;

namespace WebAPI.Data.Trigger.roles;

public class RoleTrigger:BaseTrigger<Role>
{
    public RoleTrigger(AppDbContext context) : base(context)
    {
    }
}