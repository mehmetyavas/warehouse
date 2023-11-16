using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Entity;
using WebAPI.Data.Repository.Base;

namespace WebAPI.Data.Repository;

public class UserRepository : BaseRepository<User, AppDbContext>
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetAsync(CancellationToken cancellationToken = default)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == Context.UserId, cancellationToken);

        return user;
    }
}