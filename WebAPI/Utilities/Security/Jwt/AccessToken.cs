namespace WebAPI.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public List<int> Permissions { get; set; } = new();
        public List<string> Roles { get; set; } = new();
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public string? AvatarUrl { get; set; }
    }
}