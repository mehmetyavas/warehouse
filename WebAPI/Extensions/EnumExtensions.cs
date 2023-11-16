using WebAPI.Data.Enum;

namespace WebAPI.Extensions;

public static class EnumExtensions
{
    public static string GetValueString(this ActionName value)
    {
        return ((int)value).ToString();
    }
    
    public static string Localize(this LangKeys value)
    {
        return value.ToString();
    }
}