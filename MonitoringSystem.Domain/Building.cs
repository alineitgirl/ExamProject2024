using System.Drawing;

namespace MonitoringSystem.Domain;

public class Building
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public List<Sensor> Sensors { get; set; }
}