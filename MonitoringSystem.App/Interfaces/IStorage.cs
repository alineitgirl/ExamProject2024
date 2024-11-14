namespace MonitoringSystem.App.Interfaces;

public interface IStorage<T>
{
    Task<Guid> AddAsync(T item, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
}