using Microsoft.EntityFrameworkCore;
using MonitoringSystem.App.Interfaces;
using MonitoringSystem.Data.DbContext;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.Storages;

public class UserStorage : IStorage<User>
{
    private readonly SystemDbContext _dbcontext;

    public UserStorage(SystemDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Guid.Empty;
        }
        await _dbcontext.Users.AddAsync(user, cancellationToken);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }
        return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        await  _dbcontext.Users.Where(u => u.Id == id).
           AsQueryable().ExecuteDeleteAsync(cancellationToken);
    }
}