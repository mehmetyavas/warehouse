using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity.Base;

public abstract class AbstractEntiy : IEntity
{
    public RowStatus RowStatus { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual string SortBy(string sort)
    {
        return nameof(CreatedAt);
    }

   
}