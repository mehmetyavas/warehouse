using WebAPI.Data.Entity;
using WebAPI.Data.Repository.Base;

namespace WebAPI.Data.Repository;

public class UserRolesRepository : BaseRepository<UserRole, AppDbContext>
{
    public UserRolesRepository(AppDbContext context) : base(context)
    {
    }

}