namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IDbService
{
    Task<T?> GetAsync<T>(
        string command,
        object? parms,
        CancellationToken cancellationToken = default); 
    Task<List<T>> GetListAsync<T>(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default);

    Task<int> EditDataAsync(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default);
}