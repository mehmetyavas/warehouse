using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Entity;

namespace WebAPI.Configuration;

public static class NavigationConfiguration
{
    internal static void SetAutoIncludes(this ModelBuilder builder)
    {
        var user = builder.Entity<User>();
        var userRoles = builder.Entity<UserRole>();


        user.Navigation(x => x.UserRoles).AutoInclude();
        userRoles.Navigation(x => x.Role).AutoInclude();
        userRoles.Navigation(x => x.User).AutoInclude();

        builder.Entity<Role>().Navigation(x => x.RolePermissions).AutoInclude();
        builder.Entity<RolePermission>().Navigation(x => x.Role).AutoInclude();
    }
}