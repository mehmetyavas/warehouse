using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Dto.File;

public class FileRequest:IDto
{
    public IFormFile File { get; set; } = null!;
    public FileDirectory Directory { get; set; } = FileDirectory.File;
}