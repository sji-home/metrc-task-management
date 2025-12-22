using Metrc.TaskManagement.Domain.DTOs;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IWorkTaskService
{
    Task<int> CreateTaskAsync(WorkTaskDTO workTaskDTO, CancellationToken cancellationToken = default);
    Task<List<WorkTaskResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkTaskResponseDTO?> GetWorkTaskAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> UpdateWorkTaskAsync(UpdateWorkTaskDTO updateWorkTaskDTO, CancellationToken cancellationToken = default);
    Task<bool> DeleteWorkTaskAsync(int id, CancellationToken cancellationToken = default);
}
