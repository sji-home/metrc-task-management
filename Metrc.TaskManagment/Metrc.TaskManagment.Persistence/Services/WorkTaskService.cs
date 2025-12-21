using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Persistence.Services;

public class WorkTaskService : IWorkTaskService
{
    private readonly IDbService _dbService;

    public WorkTaskService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<int> CreateTask(WorkTask workTask)
    {
        var sql = @"INSERT INTO public.work_task (status_id, assigned_user_id, title, description) 
                    VALUES (@Status, @AssignedUserId, @Title, @Description) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, workTask);
        return id;
    }

    public async Task<List<WorkTask>> GetAll()
    {
        var tasks = await _dbService.GetList<WorkTask>("SELECT id, status_id AS Status, assigned_user_id as AssignedUserId, title, description FROM public.work_task", new { });
        return tasks;
    }

    public async Task<WorkTask> GetWorkTask(int id)
    {
        var task = await _dbService.GetAsync<WorkTask>("SELECT id, status_id AS Status, assigned_user_id as AssignedUserId, title, description FROM public.work_task where id=@id", new { id });
        return task;
    }

    public async Task<bool> UpdateWorkTask(WorkTask task)
    {
        var rowsAffected =
            await _dbService.EditData(
                "Update public.work_task SET status_id=@Status, assigned_user_id=@AssignedUserId, title=@Title, description=@Description WHERE id=@Id",
                task);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteWorkTask(int id)
    {
        var rowsAffected = await _dbService.EditData("DELETE FROM public.work_task WHERE id=@Id", new { id });
        return rowsAffected > 0;
    }
}