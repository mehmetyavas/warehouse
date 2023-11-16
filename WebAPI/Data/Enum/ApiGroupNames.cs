using WebAPI.Attributes;

namespace WebAPI.Data.Enum;

public enum ApiGroupNames
{
    [GroupInfo(Description = "All Areas", Title = "All", Version = "v1")]
    All = 0,
    [GroupInfo(Description = "Default Area for global calls", Title = "Default", Version = "v1")]
    Default = 1,
    [GroupInfo(Description = "Back Office Area", Title = "Back Office", Version = "v1")]
    BackOffice = 2,
}