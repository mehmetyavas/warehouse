using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Exceptions;

public class SlugException : Exception
{
    public SlugException() : base(LangKeys.SlugExistException.Localize())
    {
    }

    public SlugException(string message) : base(message)
    {
    }
}