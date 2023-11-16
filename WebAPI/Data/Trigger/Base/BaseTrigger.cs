using EntityFrameworkCore.Triggered;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Trigger.Base;

public abstract class BaseTrigger<T> : IBeforeSaveTrigger<T>
    where T : class, IEntity
{
    protected readonly AppDbContext Context;

    protected BaseTrigger(AppDbContext context)
    {
        Context = context;
    }


    public virtual async Task BeforeSave(ITriggerContext<T> context, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var entityEntry = Context.Entry(context.Entity);

        switch (context.ChangeType)
        {
            case ChangeType.Added:
                if (entityEntry.Entity.CreatedAt == DateTime.MinValue)
                {
                    entityEntry.Entity.CreatedAt = now;
                }

                if (entityEntry.Entity.Modified == DateTime.MinValue)
                {
                    entityEntry.Entity.Modified = now;
                }

                entityEntry.Entity.RowStatus = RowStatus.Active;
                break;
            case ChangeType.Modified:
                if (entityEntry.Entity.Modified == DateTime.MinValue)
                {
                    entityEntry.Entity.Modified = now;
                }

                entityEntry.Entity.Modified = now;
                break;
            case ChangeType.Deleted:
                break;
        }

        await Task.CompletedTask;
    }
}