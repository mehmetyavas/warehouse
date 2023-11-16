using WebAPI.Data.Enum;
using WebAPI.Extensions;

namespace WebAPI.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base(LangKeys.NotFound.Localize())
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}