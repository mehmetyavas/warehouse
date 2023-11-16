using WebAPI.Extensions;

namespace WebAPI.Services.User;

public class UserResolverServices
{
    public int UserId { get; set; }

    public UserResolverServices(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext is not null)
            if (httpContextAccessor.HttpContext.User.CheckAuthenticate(out var userId))
                UserId = userId;
    }
}