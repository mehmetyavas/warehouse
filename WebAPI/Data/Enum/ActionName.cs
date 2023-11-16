using WebAPI.Attributes;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace WebAPI.Data.Enum;

public enum ActionName
{
    [ActionProperties(ControllerName.WeatherForecast, HttpMethod.Get, commonAction: true)]
    WeatherForecast,


    [ActionProperties(ControllerName.Role, HttpMethod.Get)]
    ActionList,

    [ActionProperties(ControllerName.Role, HttpMethod.Get)]
    RoleList,

    [ActionProperties(ControllerName.Role, HttpMethod.Get)]
    RoleUserList,

    [ActionProperties(ControllerName.Role, HttpMethod.Post)]
    RolePermissionCreate,

    [ActionProperties(ControllerName.Role, HttpMethod.Post)]
    RolePermissionDelete,


    [ActionProperties(ControllerName.User, HttpMethod.Get)]
    UserGet,

    [ActionProperties(ControllerName.User, HttpMethod.Put)]
    UserUpdate,

    [ActionProperties(ControllerName.User, HttpMethod.Put)]
    UserAvatar,

    [ActionProperties(ControllerName.User, HttpMethod.Delete)]
    UserDelete,


}