namespace MonitoringSystem.Domain;

public class Sensor
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    public List<SensorReading> Readings { get; set; }
    public Guid BuildingId { get; set; }
    public Building Building { get; set; }
}