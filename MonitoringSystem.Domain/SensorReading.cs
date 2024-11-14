namespace MonitoringSystem.Domain;

public class SensorReading
{
    public double BatteryChargeLevel { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    public Guid SensorId { get; set; }
    public Sensor Sensor { get; set; }
}