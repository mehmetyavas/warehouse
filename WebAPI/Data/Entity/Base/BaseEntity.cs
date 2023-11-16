using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Entity.Base;

public abstract class BaseEntity : BaseEntity<int>
{
}

public abstract class BaseEntity<T> : AbstractEntiy
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.Now;
        RowStatus = RowStatus.Active;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; } = default!;

}