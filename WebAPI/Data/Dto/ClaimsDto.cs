using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto;

public class ClaimsDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<int> Roles { get; set; } = new();
    
    public List<int> Permissions { get; set; } = new();

    public bool IsAdmin()
    {
        return Roles.Contains((int)Enum.Roles.Admin);
    }
}