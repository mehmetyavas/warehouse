using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.ActionFilters;

public class PermissionFilter : IActionFilter
{
    public ICollection<ActionName> ActionNames { get; }

    public PermissionFilter(ICollection<ActionName> actionNames)
    {
        ActionNames = actionNames;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!ActionNames.Any()) return;
        var userPermissions = context.HttpContext.User.GetPermissions();


        if (!userPermissions.Any(x => ActionNames.Contains(x)))
        {
            throw new Exception(LangKeys.LackOfPermission.Localize());
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}