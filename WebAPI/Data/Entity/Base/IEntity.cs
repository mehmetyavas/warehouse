using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity.Base;

public interface IEntity
{
    public RowStatus RowStatus { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string SortBy(string sort);
}