using System.Reflection;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;
using WebAPI.Exceptions;

namespace WebAPI.Extensions;


public static class BaseEntityExtensions
{
    public static bool IsSortableString(this AbstractEntiy entity, string sortBy)
    {
        return typeof(AbstractEntiy).GetProperty(sortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
    }

    public static void CheckNull(this AbstractEntiy entity, string? message=null)
    {
        if (entity is null)
        {
            throw new NotFoundException(message?? LangKeys.NoRecordFound.Localize());
        }
    }
}