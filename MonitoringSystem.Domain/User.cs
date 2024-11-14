namespace MonitoringSystem.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public List<Building> Buildings { get; set; }
}