using EntityFrameworkCore.Triggered;
using WebAPI.Data.Entity;
using WebAPI.Data.Trigger.Base;

namespace WebAPI.Data.Trigger.UserTriggers;

public class UserBeforeTrigger :BaseTrigger<User>
{
    public UserBeforeTrigger(AppDbContext context) : base(context)
    {
    }


    public override Task BeforeSave(ITriggerContext<User> context, CancellationToken cancellationToken)
    {
        return base.BeforeSave(context, cancellationToken);
    }
}