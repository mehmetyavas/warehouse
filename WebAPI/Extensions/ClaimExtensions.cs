using System.Security.Claims;
using WebAPI.Data.Enum;

namespace WebAPI.Extensions;

public static class ClaimExtensions
{
    public static void AddId(this ICollection<Claim> claims, string userId)
    {
        claims.Add(new Claim(JwtClaimTypes.Id.ToString(), userId));
    }

    public static void AddId(this ICollection<Claim> claims, Guid userId)
    {
        claims.Add(new Claim(JwtClaimTypes.Id.ToString(), userId.ToString()));
    }

    public static void AddName(this ICollection<Claim> claims, string name)
    {
        claims.Add(new Claim(JwtClaimTypes.Name.ToString(), name));
    }

    public static void AddAvatar(this ICollection<Claim> claims, string avatar)
    {
        claims.Add(new Claim(JwtClaimTypes.AvatarUrl.ToString(), avatar));
    }

    public static void AddUserName(this ICollection<Claim> claims, string userName)

    {
        claims.Add(new Claim(JwtClaimTypes.UserName.ToString(), userName));
    }

    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new Claim(JwtClaimTypes.Email.ToString(), email));
    }


    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList()
            .ForEach(role =>
                claims.Add(new Claim(JwtClaimTypes.Role.ToString(), role)));
    }

    public static void AddPermissions(this ICollection<Claim> claims, string[] permissions)
    {
        permissions.ToList()
            .ForEach(permission =>
                claims.Add(new Claim(JwtClaimTypes.Permission.ToString(), permission)));
    }
}