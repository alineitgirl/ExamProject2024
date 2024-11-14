using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.EntityConfigurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("Buildings");
        
        builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(500);
        builder.Property(b => b.Address).HasMaxLength(100).IsRequired();
        
        builder.HasKey(c => c.Id);
        builder.HasMany<Sensor>().WithOne(b => b.Building).HasForeignKey(s => s.BuildingId);
        builder.HasOne<User>().WithMany(u => u.Buildings).HasForeignKey(s => s.UserId);
    }
}