using WebAPI.Data.Enum;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace WebAPI.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ActionPropertiesAttribute : Attribute
{
    public ActionPropertiesAttribute(ControllerName controllerName, HttpMethod method, bool commonAction = false)
    {
        ControllerName = controllerName;
        Method = method;
        CommonAction = commonAction;
    }

    public bool CommonAction { get; set; }
    public ControllerName ControllerName { get; init; }
    public HttpMethod Method { get; init; }
}