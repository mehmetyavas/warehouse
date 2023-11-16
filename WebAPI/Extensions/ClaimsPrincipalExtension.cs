using System.Security.Claims;
using WebAPI.Data.Dto;
using WebAPI.Data.Enum;

namespace WebAPI.Extensions;

public static class ClaimsPrincipalExtension
{
    public static List<string> GetTokenProps(this ClaimsPrincipal claims, string key)
    {
        if (!claims.Claims.Any())
        {
            return new List<string>();
        }
        return claims.Claims.Where(x => x.Type.EndsWith(key))!.Select(x => x.Value).ToList();
    }

    public static List<ActionName> GetPermissions(this ClaimsPrincipal user)
    {
        var permissions = user.Claims.Where(c => c.Type == JwtClaimTypes.Permission.ToString());
        return permissions.Select(x => (ActionName)System.Enum.Parse(typeof(ActionName), x.Value)).ToList();
    }

    public static bool CheckAuthenticate(this ClaimsPrincipal? claim, out int userId)
    {
        if (claim == null)
        {
            userId = 0;
            return false;
        }

        if (!claim.Identity!.IsAuthenticated)
        {
            userId = 0;
            return false;
        }

        userId = Convert
            .ToInt32(claim.GetTokenProps(JwtClaimTypes.Id.ToString()).First());
        return true;
    }

    public static ClaimsDto GetClaimsDto(this ClaimsPrincipal? claims)
    {
        return new ClaimsDto
        {
            Id = Convert.ToInt32(claims?.GetTokenProps(JwtClaimTypes.Id.ToString()).First()),
            Name = claims?.GetTokenProps(JwtClaimTypes.Name.ToString()).First()!,
            Email = claims?.GetTokenProps(JwtClaimTypes.Email.ToString()).First()!,
            Roles = claims?.GetTokenProps(JwtClaimTypes.Role.ToString())
                .Select(x => Convert.ToInt32(x))
                .ToList()!,
            Permissions = claims?.GetTokenProps(JwtClaimTypes.Permission.ToString())
                .Select(x => Convert.ToInt32(x))
                .ToList()!
        };
    }
}