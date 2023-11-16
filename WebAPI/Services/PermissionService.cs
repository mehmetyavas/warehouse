using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using WebAPI.Attributes;
using WebAPI.Data;
using WebAPI.Data.Entity;
using WebAPI.Data.Enum;

namespace WebAPI.Services;

public class PermissionService
{
    private AppDbContext _context;

    public PermissionService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    private async Task ClearClaims()
    {
        await _context.RolePermissions.ExecuteDeleteAsync();
    }

    // can't touch this 🎵
    public async Task CreateBasePermission()
    {
        await ClearClaims();

        var actionNames = Enum.GetValues(typeof(ActionName)).Cast<ActionName>();

        var actionQuery = _context.RolePermissions.AsQueryable();

        foreach (var item in actionNames)
        {
            var action = await actionQuery
                .FirstOrDefaultAsync(x => x.ActionId == item);


            if (action is not null)
                continue;

            var attribute = item.GetAttributeOfType<ActionPropertiesAttribute>();


            _context.RolePermissions.Add(new RolePermission
            {
                RowStatus = RowStatus.Active,
                RoleId = Roles.Admin,
                ActionId = item,
            });


            if (attribute?.CommonAction == true)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    CreatedAt = DateTime.Now,
                    RoleId = Roles.User,
                    ActionId = item
                });
            }
        }

        var recordsToDelete =
            await actionQuery
                .Where(x => !actionNames.ToList().Contains(x.ActionId))
                .ToListAsync();


        foreach (var item in recordsToDelete)
        {
            _context.RolePermissions.Remove(item);
        }


        await _context.SaveChangesAsync();
    }
}