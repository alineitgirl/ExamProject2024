using AutoMapper;
using MonitoringSystem.App.Dto;
using MonitoringSystem.App.Interfaces;
using MonitoringSystem.Domain;

namespace MonitoringSystem.App.Services;

public class UserService
{
    private readonly IStorage<Building> _buildingStorage;
    private readonly IStorage<User> _userStorage;
    private readonly IStorage<Sensor> _sensorStorage;
    private readonly IMapper _mapper;
    
    public UserService(IStorage<Building> buildingStorage, IStorage<User> userStorage, IStorage<Sensor> sensorStorage,
        IMapper mapper)
    {
        _buildingStorage = buildingStorage;
        _userStorage = userStorage;
        _sensorStorage = sensorStorage;
        _mapper = mapper;
    }
    
    public async Task<Guid> AddBuildingAsync(NewBuildingDto buildingDto, CancellationToken cancellationToken = default)
    {
        var newBuilding = _mapper.Map<Building>(buildingDto);
        await _buildingStorage.AddAsync(newBuilding, cancellationToken);
        return newBuilding.Id;
    }

    public async Task CheckBatteryLevel(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        while (true)
        {
            foreach (var sensor in sensorStorage.GetAll())
            {
                var user = _sensorStorage.CheckBatteryLevelAsync(sensor.Id, cancellationToken);
                if (user != null)
                {
                    user.SendNotification(user, message);
                }
            }
        }
    }

    public async Task<string> NotificateUser(User user, string message)
    {
        NotificationService notificationService = new NotificationService();
        await notificationService.SendNotificationAsync(user.Email, message, cancellationToken);
    }
}