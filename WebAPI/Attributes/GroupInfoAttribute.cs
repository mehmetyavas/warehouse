namespace WebAPI.Attributes;

public class GroupInfoAttribute : Attribute
{
    public string Title { get; set; } = null!;
    public string Version { get; set; } = null!;
    public string Description { get; set; } = null!;
}