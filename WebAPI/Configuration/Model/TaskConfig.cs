namespace WebAPI.Configuration.Model;

public class TaskConfig
{
    public ConfigEntity Group { get; set; } = null!;
    public ConfigEntity Todo { get; set; } = null!;
    public ConfigEntity Task { get; set; } = null!;
}