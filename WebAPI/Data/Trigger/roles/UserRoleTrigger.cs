using WebAPI.Data.Entity;
using WebAPI.Data.Trigger.Base;

namespace WebAPI.Data.Trigger.roles;

public class UserRoleTrigger:BaseTrigger<UserRole>
{
    public UserRoleTrigger(AppDbContext context) : base(context)
    {
    }
}