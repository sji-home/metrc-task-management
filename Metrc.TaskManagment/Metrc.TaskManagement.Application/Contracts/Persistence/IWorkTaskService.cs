using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Application.Contracts.Persistence;

public interface IWorkTaskService
{
    Task<int> CreateTask(WorkTask workTask);
    Task<List<WorkTask>> GetAll();
    //Task<List<Task>> GetTasksForUser();
    Task<WorkTask> GetWorkTask(int id);
    Task<bool> UpdateWorkTask(WorkTask workTask);
    Task<bool> DeleteWorkTask(int id);
}
