using Microsoft.EntityFrameworkCore;
using MonitoringSystem.App.Interfaces;
using MonitoringSystem.Data.DbContext;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.Storages;

public class BuildingStorage : IStorage<Building>
{
    private readonly SystemDbContext _dbContext;

    public BuildingStorage(SystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddBuildingAsync(Guid userId, Building building, CancellationToken cancellationToken = default)
    {
        building.Id = userId;
        await _dbContext.Buildings.AddAsync(building, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Building?> GetBuildingAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }

        return await _dbContext.Buildings.Where(b => b.Id == id && b.UserId == userId).AsQueryable()
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task DeleteBuildingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        } 
        await _dbContext.Buildings.Where(c => c.Id == id).ExecuteDeleteAsync(cancellationToken);
    }   
}