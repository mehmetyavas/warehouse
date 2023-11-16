namespace WebAPI.Data.Dto.ConfigDtos;

public class AppInfo
{
    public string AppName { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string AppSite { get; set; } = null!;

    public string BaseUrl { get; set; } = null!;

    public string AppUrl { get; set; } = null!;
}