using WebAPI.Data.Entity;
using WebAPI.Data.Repository.Base;

namespace WebAPI.Data.Repository;

public class RoleRepository : BaseRepository<Role, AppDbContext>
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

   
}