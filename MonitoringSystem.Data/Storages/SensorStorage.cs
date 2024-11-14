using Microsoft.EntityFrameworkCore;
using MonitoringSystem.App.Interfaces;
using MonitoringSystem.Data.DbContext;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.Storages;

public class SensorStorage : IStorage<Sensor>
{
    private readonly SystemDbContext _dbcontext;

    public async void AddAsync(Sensor sensor, Guid buildingId, CancellationToken cancellationToken = default) 
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }
        sensor.BuildingId = buildingId;
        await _dbcontext.Sensors.AddAsync(sensor);
        await _dbcontext.SaveChangesAsync(cancellationToken);   
    }

    public async Task<Sensor?> GetByIdAsync(Guid id, Guid buildingId, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return null;
        }
        return await _dbcontext.Sensors.Where(s => s.Id == id && s.BuildingId == buildingId).
            FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> CheckBatteryLevelAsync(Guid sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _dbcontext.Sensors
            .Include(s => s.Building)
            .ThenInclude(b => b.User)
            .FirstOrDefaultAsync(s => s.Id == sensorId, cancellationToken);

        if (sensor == null) return null;

        foreach (var reading in sensor.Readings)
        {
            if (reading.BatteryChargeLevel < 10)
                return sensor.Building.User;
        }

        return null;
    }
}  