using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.EntityConfigurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.ToTable("sensors");
       builder.Property(s => s.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
       builder.Property(s => s.Description).HasMaxLength(500).HasColumnName("description");
       builder.Property(s => s.Latitude).HasColumnName("latitude").IsRequired();
       builder.Property(s => s.Longitude).HasColumnName("longitude").IsRequired();
       builder.Property(s => s.ImageUrl).HasColumnName("image_url");
       
       builder.HasKey(s => s.Id);
       builder.HasMany<SensorReading>().WithOne(s => s.Sensor).HasForeignKey(s => s.SensorId);
       
       builder.HasOne<Building>().WithMany(b => b.Sensors).HasForeignKey(s => s.BuildingId);
    }
}