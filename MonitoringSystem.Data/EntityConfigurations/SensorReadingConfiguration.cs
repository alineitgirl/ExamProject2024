using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.EntityConfigurations;

public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
{
    public void Configure(EntityTypeBuilder<SensorReading> builder)
    {
        builder.ToTable("sensor_reading");
        
        builder.Property(s => s.SensorId).IsRequired();
        builder.Property(s => s.Temperature).IsRequired();
        builder.Property(s => s.Humidity).IsRequired();
        builder.Property(s => s.BatteryChargeLevel).IsRequired();
        
        builder.HasOne<Sensor>().WithMany(s => s.Readings).HasForeignKey(s => s.SensorId);
    }
}