using WebAPI.Data.Entity;
using WebAPI.Data.Repository.Base;

namespace WebAPI.Data.Repository;

public class RolePermissionRepository : BaseRepository<RolePermission, AppDbContext>
{
    public RolePermissionRepository(AppDbContext context) : base(context)
    {
    }
}