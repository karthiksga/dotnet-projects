using CachingStrategies.Domain.Users;
using CachingStrategies.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CachingStrategies.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        context.Users.Remove(new User { Id = id });
        await context.SaveChangesAsync();
    }
}