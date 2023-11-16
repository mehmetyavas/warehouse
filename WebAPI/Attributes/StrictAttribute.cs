using WebAPI.Data.Enum;

namespace WebAPI.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class StrictAttribute : Attribute
{
    public bool IsStrict { get; set; }
    public Roles Role { get; set; }

    public StrictAttribute()
    {
        Role = Roles.Admin;
        IsStrict = true;
    }

    public StrictAttribute(Roles role)
    {
        Role = role;
        IsStrict = true;
    }
}