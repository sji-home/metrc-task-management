using Metrc.TaskManagement.Application.Contracts.Persistence;

namespace Metrc.TaskManagement.Persistence.Services;

public class TaskService : ITaskService
{
    private readonly IDbService _dbService;

    public TaskService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<int> CreateTask(Task task)
    {
        var sql = @"INSERT INTO public.Task (status_id, assigned_user_id, description) 
                    VALUES (@Status, @AssignedUserId, @Description) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, task);
        return id;
    }

    public async Task<List<Task>> GetAll()
    {
        var tasks = await _dbService.GetAll<Task>("SELECT id, status_id AS Status, assigned_user_id, description FROM public.Task", new { });
        return tasks;
    }


    public async Task<Task> GetTask(int id)
    {
        var task = await _dbService.GetAsync<Task>("SELECT id, status_id AS Status, assigned_user_id, description FROM public.Task where id=@id", new { id });
        return task;
    }

    public async Task<bool> UpdateTask(Task task)
    {
        var rowsAffected =
            await _dbService.EditData(
                "Update public.Task SET status_id=@Status, assigned_user_id=@AssignedUserId, description=@Description WHERE id=@Id",
                task);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var rowsAffected = await _dbService.EditData("DELETE FROM public.Task WHERE id=@Id", new { id });
        return rowsAffected > 0;
    }
}