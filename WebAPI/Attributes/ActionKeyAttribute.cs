using WebAPI.Data.Enum;

namespace WebAPI.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ActionKeyAttribute : Attribute
{
    public ICollection<ActionName> Permissions { get; set; } = new List<ActionName>();

    public ActionKeyAttribute(ICollection<ActionName> permissions)
    {
        Permissions = permissions;
    }

    public ActionKeyAttribute(ActionName actionName)
    {
        Permissions.Add(actionName);
    }
}