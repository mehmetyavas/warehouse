using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Configuration.Model;
using WebAPI.Data.Entity;
using WebAPI.Extensions;
using WebAPI.Utilities.Helpers;
using Claim = System.Security.Claims.Claim;

namespace WebAPI.Utilities.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
    }

    public IConfiguration Configuration { get; }

    public static string DecodeToken(string input)
    {
        var handler = new JwtSecurityTokenHandler();
        if (input.StartsWith("Bearer "))
        {
            input = input["Bearer ".Length..];
        }

        return handler.ReadJwtToken(input).ToString();
    }

    public TAccessToken CreateToken<TAccessToken>(User user)
        where TAccessToken : IAccessToken, new()
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey!);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new TAccessToken()
        {
            Token = token,
            Expiration = _accessTokenExpiration,
            RefreshToken = GenerateRefreshToken()
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
        User user,
        SigningCredentials signingCredentials)
    {
        var jwt = new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user),
            signingCredentials: signingCredentials);
        return jwt;
    }


    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private static IEnumerable<Claim> SetClaims(User user)
    {
        var claims = new List<Claim>();
        claims.AddId(user.Id.ToString());


        if (!string.IsNullOrEmpty(user.FullName))
            claims.AddName(user.FullName);


        if (!string.IsNullOrWhiteSpace(user.Email))
            claims.AddEmail(user.Email);


        claims.AddRoles(user.Roles().ToArray());

        if (user.IsAdmin())
        {
            claims.AddPermissions(new string[]
            {
            });
        }
        else
            claims.AddPermissions(user.ActionIds().ToArray());


        return claims;
    }
}