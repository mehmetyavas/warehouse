using WebAPI.Data.Entity;

namespace WebAPI.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        TAccessToken CreateToken<TAccessToken>(User user)
            where TAccessToken : IAccessToken, new();

        string GenerateRefreshToken();
    }
}