using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Configuration;
using WebAPI.Data.Entity;
using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;
using WebAPI.Services.User;
using WebAPI.Utilities.Helpers;

namespace WebAPI.Data;

public class AppDbContext : DbContext
{
    public int UserId { get; set; }


    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        UserResolverServices userResolver, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
        UserId = userResolver.UserId;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        base.OnConfiguring(optionsBuilder.UseNpgsql(AppConfig.DevConnectionString));

        optionsBuilder.UseTriggers(x =>
        {
            // x.AddTrigger<UserBeforeTrigger>();
            // x.AddTrigger<CategoryBeforeTrigger>();
            // x.AddTrigger<ProductBeforeTrigger>();
            // x.AddTrigger<RoleTrigger>();
            // x.AddTrigger<RolePermissionTrigger>();
            // x.AddTrigger<UserRoleTrigger>();
            // x.AddTrigger<ProductImageBeforeTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var entities = builder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.GetInterface(nameof(IEntity)) != null)
            .Select(e => e.ClrType);


        foreach (var entity in entities)
        {
            var parameter = Expression.Parameter(entity, "x");
            var property = Expression.Property(parameter, nameof(RowStatus));
            var constant = Expression.Constant(RowStatus.Active);
            var body = Expression.Equal(property, constant);
            var lambda = Expression.Lambda(body, parameter);

            builder.Entity(entity).HasQueryFilter(lambda);

            builder.Entity(entity).Property(nameof(IEntity.CreatedAt)).HasDefaultValue(DateTime.Now);

            builder.Entity(entity).Property(nameof(IEntity.RowStatus)).HasDefaultValue(RowStatus.Active);
        }

        builder.SetAutoIncludes();

        builder.Seed();
        // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}