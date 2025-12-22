namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IDbService
{
    Task<T?> GetAsync<T>(
        string command,
        object? parms,
        CancellationToken cancellationToken = default); 
    Task<List<T>> GetList<T>(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default);

    Task<int> EditData(
        string command, 
        object? parms,
        CancellationToken cancellationToken = default);
}