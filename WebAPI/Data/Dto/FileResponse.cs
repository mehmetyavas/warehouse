using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class UploaderResponse : IDto
{
    public string Name { get; set; } = null!;
    public long Size { get; set; }
}