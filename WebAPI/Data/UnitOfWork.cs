using WebAPI.Data.Repository;

namespace WebAPI.Data;

public class UnitOfWork : IDisposable
{
    public int UserId { get; set; }
    private readonly AppDbContext _context;

    private readonly UserRepository _userRepository;
    private readonly RoleRepository _roleRepository;
    private readonly UserRolesRepository _userRolesRepository;
    private readonly RolePermissionRepository _rolePermissionRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        UserId = context.UserId;
    }


    public void Dispose()
    {
        _context?.Dispose();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }


    public UserRepository Users => _userRepository ?? new(_context);

    public RoleRepository Roles => _roleRepository ?? new(_context);

    public UserRolesRepository UserRoles => _userRolesRepository ?? new(_context);


    public RolePermissionRepository RolePermissions => _rolePermissionRepository ?? new(_context);
}