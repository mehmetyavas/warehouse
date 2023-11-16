using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Attributes;
using WebAPI.Extensions;

namespace WebAPI.ActionFilters;

public class BaseFilterProvider : IFilterProvider
{
    public void OnProvidersExecuting(FilterProviderContext context)
    {
        if (context.ActionContext.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
        {
            var controllerType = actionDescriptor.ControllerTypeInfo;
            var permissionAttribute = controllerType.GetCustomAttribute<PermissionAttribute>();

            var anonymousAttribute = actionDescriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>()
                ?? controllerType.GetCustomAttribute<AllowAnonymousAttribute>();
            
            
            if (permissionAttribute is not null &&
                anonymousAttribute is null)
            {
                var user = context.ActionContext.HttpContext.User.GetClaimsDto();

                if (!user.IsAdmin())
                {
                    var actionKeyAttributes =
                        actionDescriptor.MethodInfo.GetCustomAttributes<ActionKeyAttribute>().ToList();


                    foreach (var actionKeyAttribute in actionKeyAttributes)
                    {
                        var permissionFilter = new PermissionFilter(actionKeyAttribute.Permissions);
                        context.Results.Add(new FilterItem(
                            new FilterDescriptor(permissionFilter, FilterScope.Action),
                            permissionFilter));
                    }
                }
            }
        }
    }

    public void OnProvidersExecuted(FilterProviderContext context)
    {
    }

    public int Order { get; } = 0;
}