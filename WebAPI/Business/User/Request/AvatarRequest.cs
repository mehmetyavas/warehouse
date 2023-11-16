using WebAPI.Attributes.CustomAttributes;
using WebAPI.Data.Enum;

namespace WebAPI.Business.User.Request;

public class AvatarRequest
{
    [MaxFileSize((long)FileSize.Mb)] public IFormFile File { get; set; } = null!;
}