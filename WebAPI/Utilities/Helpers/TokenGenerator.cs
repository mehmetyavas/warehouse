using System.Security.Cryptography;

namespace WebAPI.Utilities.Helpers;

public static class TokenGenerator
{
    public static string CreateVerifyToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    public static long CreateLoginCode() => new Random().NextInt64(100000, 999999);
}