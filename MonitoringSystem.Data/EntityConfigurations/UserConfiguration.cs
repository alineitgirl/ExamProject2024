using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("users");
       
       builder.Property(b => b.Name).IsRequired()
           .HasColumnName("name").HasMaxLength(50);
       builder.Property(b => b.Email).IsRequired().HasColumnName("email");
       builder.Property(b => b.PhoneNumber).HasColumnName("phoneNumber");
       builder.Property(b => b.DateOfBirth).HasColumnName("dateOfBirth");
       
       builder.HasKey(b => b.Id);
       builder.HasMany<Building>()
           .WithOne(b => b.User).HasForeignKey(b => b.UserId);
    }
}