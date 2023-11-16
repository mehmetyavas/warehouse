using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Entity.Base;

public class BaseImage : BaseEntity<Guid>
{
    [Required] public string ImageUrl { get; set; } = null!;
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Order  { get; set; }

    public bool IsMainImage { get; set; }
}