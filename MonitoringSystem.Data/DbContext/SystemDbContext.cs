using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonitoringSystem.Data.EntityConfigurations;
using MonitoringSystem.Domain;

namespace MonitoringSystem.Data.DbContext;

public class SystemDbContext : Microsoft.EntityFrameworkCore.DbContext
{
   public DbSet<User> Users => Set<User>();
   public DbSet<Building> Buildings => Set<Building>();
   public DbSet<Sensor> Sensors => Set<Sensor>();
   public DbSet<SensorReading> SensorReadings => Set<SensorReading>();
   
   public SystemDbContext() {
      Database.EnsureCreated();
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      var config = new ConfigurationBuilder()
         .SetBasePath(AppContext.BaseDirectory)
         .AddJsonFile("DbContext/appsettings.json")
         .Build();
      
      optionsBuilder.UseNpgsql(config.GetConnectionString("SystemDbContext"));
   }
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new BuildingConfiguration());
      modelBuilder.ApplyConfiguration(new SensorConfiguration());
      modelBuilder.ApplyConfiguration(new SensorReadingConfiguration());
      
      base.OnModelCreating(modelBuilder);
   }
}