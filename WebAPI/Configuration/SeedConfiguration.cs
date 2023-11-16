using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Entity;
using WebAPI.Data.Enum;
using WebAPI.Services;
using WebAPI.Utilities.Helpers;
using WebAPI.Utilities.Security.Hashing;

namespace WebAPI.Configuration;

public static class SeedConfiguration
{
    public static void Seed(this ModelBuilder builder)
    {
        RoleSeed(builder);

        UserSeed(builder);

        RoleClaimsSeed(builder);
    }


    public static async Task ActionData(this IApplicationBuilder app)
    {
        await using var provider = ServiceTool.ServiceProvider!.CreateAsyncScope();
        var actions = provider.ServiceProvider.GetService<PermissionService>();

        await actions!.CreateBasePermission();

        await provider.DisposeAsync();
    }


    #region Seeds

    private static void UserSeed(ModelBuilder builder)
    {
        HashingHelper.CreatePasswordHash("123admin123", passwordHash: out var hash, passwordSalt: out var salt);

        var now = DateTime.Now;
        builder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                RowStatus = RowStatus.Active,
                FullName = "Mehmet Emin Yavaş",
                Email = "mehmeteminyavas@magicstarling.com",
                MobilePhones = "5055555556",
                Gender = Gender.Male,
                AvatarUrl = null,
                BirthDate = new DateOnly(now.Year, now.Month, now.Day),
                RefreshToken = null,
                IsVerified = true,
                VerifyToken = "admin",
                VerifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                LoginCode = 111111,
                LoginCodeExpiredAt = DateTime.Now.AddYears(2)
            });

        builder.Entity<User>()
            .HasData(new User
            {
                Id = 2,
                RowStatus = RowStatus.Active,
                FullName = "Emrecan Ünlü",
                Email = "emrecanunlu@magicstarling.com",
                MobilePhones = "5055555554",
                Gender = Gender.Male,
                AvatarUrl = null,
                BirthDate = new DateOnly(now.Year, now.Month, now.Day),
                RefreshToken = null,
                IsVerified = true,
                VerifyToken = "admin",
                VerifiedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                LoginCode = 222222,
                LoginCodeExpiredAt = DateTime.Now.AddYears(2)
            });
    }

    private static void RoleSeed(ModelBuilder builder)
    {
        var entity = builder.Entity<Role>();

        entity.HasData(new List<Role>
        {
            new Role
            {
                Id = Roles.Admin,
                Name = Roles.Admin.ToString(),
                Description = "All Permission",
                CreatedAt = DateTime.Now
            },
            new Role
            {
                Id = Roles.Staff,
                Name = Roles.Staff.ToString(),
                Description = null,
                CreatedAt = DateTime.Now
            },
            new Role
            {
                Id = Roles.User,
                Name = Roles.User.ToString(),
                Description = null,
                CreatedAt = DateTime.Now
            }
        });
    }

    private static void RoleClaimsSeed(ModelBuilder builder)
    {
        var entity = builder.Entity<UserRole>();

        entity.HasData(new List<UserRole>
        {
            new UserRole
            {
                Id = 1,
                RoleId = Roles.Admin,
                UserId = 1,
                CreatedAt = DateTime.Now
            },
            new UserRole
            {
                Id = 2,
                RoleId = Roles.Admin,
                UserId = 2,
                CreatedAt = DateTime.Now
            }
        });
    }

    #endregion
}