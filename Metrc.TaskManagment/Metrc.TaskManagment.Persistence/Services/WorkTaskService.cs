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

    public async Task<int> CreateTaskAsync(
        WorkTask workTask, 
        CancellationToken cancellationToken = default)
    {
        var sql = @"INSERT INTO public.work_task (status_id, assigned_user_id, title, description) 
                    VALUES (@Status, @AssignedUserId, @Title, @Description) RETURNING id";

        var id = await _dbService.GetAsync<int>(sql, workTask, cancellationToken);
        return id;
    }

    public async Task<List<WorkTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sql =
            """
            SELECT id, status_id AS Status, assigned_user_id as AssignedUserId, title, description 
            FROM public.work_task
            """;

        var tasks = await _dbService.GetListAsync<WorkTask>(sql, new { }, cancellationToken);
        return tasks;
    }

    public async Task<WorkTask?> GetWorkTaskAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var sql =
            """
            SELECT id,
                    status_id AS Status,
                    assigned_user_id AS AssignedUserId,
                    title,
                    description
            FROM public.work_task
            WHERE id = @id
            """;

        return await _dbService.GetAsync<WorkTask>(
            sql, 
            new { id }, 
            cancellationToken);
    }

    public async Task<bool> UpdateWorkTaskAsync(
        WorkTask task, 
        CancellationToken cancellationToken = default)
    {
        var sql =
           """
           Update public.work_task 
           SET status_id=@Status, assigned_user_id=@AssignedUserId, title=@Title, description=@Description 
           WHERE id=@Id
           """;

        var rowsAffected =
            await _dbService.EditDataAsync(
                sql,
                task,
                cancellationToken);

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteWorkTaskAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var sql =
           """
           DELETE FROM public.work_task WHERE id=@Id
           """; 

        var rowsAffected = await _dbService.EditDataAsync(
            sql, 
            new { id }, 
            cancellationToken);

        return rowsAffected > 0;
    }
}