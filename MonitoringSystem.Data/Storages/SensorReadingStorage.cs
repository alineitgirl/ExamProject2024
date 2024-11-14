using Microsoft.EntityFrameworkCore;
using MonitoringSystem.App.Interfaces;
using MonitoringSystem.Data.DbContext;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.Storages;

public class SensorReadingStorage : IStorage<SensorReading>
{
    private readonly SystemDbContext _context;

    public SensorReadingStorage(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddSensorReadingAsync(SensorReading sensorReading, 
        double minValue, double maxValue, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Guid.Empty;
        }
        if (sensorReading.Temperature > maxValue || sensorReading.Temperature < minValue) return Guid.Empty;
        await _context.SensorReadings.AddAsync(sensorReading, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sensorReading.SensorId;
    }
    
    
}