using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IWorkTaskService
{
    Task<int> CreateTaskAsync(WorkTask workTask, CancellationToken cancellationToken = default);
    Task<List<WorkTask>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkTask?> GetWorkTaskAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> UpdateWorkTaskAsync(WorkTask workTask, CancellationToken cancellationToken = default);
    Task<bool> DeleteWorkTaskAsync(int id, CancellationToken cancellationToken = default);
}
