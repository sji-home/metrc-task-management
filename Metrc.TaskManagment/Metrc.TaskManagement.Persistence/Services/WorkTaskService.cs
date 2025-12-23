using Metrc.TaskManagement.Application.Contracts.Persistence;
using Metrc.TaskManagement.Application.Profiles.Mappings;
using Metrc.TaskManagement.Domain.DTOs;
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
        WorkTaskDTO workTaskDTO, 
        CancellationToken cancellationToken = default)
    {
        var sql = @"INSERT INTO public.work_task (status_id, assigned_user_id, title, description) 
                    VALUES (@Status, @AssignedUserId, @Title, @Description) RETURNING id";

        var workTask = DTOToEntityMapper.ToEntity(workTaskDTO);

        var id = await _dbService.GetAsync<int>(sql, workTask, cancellationToken);
        return id;
    }

    public async Task<List<WorkTaskResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sql =
            """
            SELECT id, status_id AS Status, assigned_user_id as AssignedUserId, title, description 
            FROM public.work_task
            """;

        var workTasks = await _dbService.GetListAsync<WorkTask>(sql, new { }, cancellationToken);

        var workTasksDTO = EntityToDTOMapper.ToResponseDTO(workTasks);

        return workTasksDTO ?? [];
    }

    public async Task<WorkTaskResponseDTO?> GetWorkTaskAsync(
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

        var workTask = await _dbService.GetAsync<WorkTask>(sql, new { id }, cancellationToken);

        WorkTaskResponseDTO? workTaskResponseDTO = null;

        if (workTask is not null)
            workTaskResponseDTO = EntityToDTOMapper.ToResponseDTO(workTask);

        return workTaskResponseDTO;
    }

    public async Task<bool> UpdateWorkTaskAsync(
        UpdateWorkTaskDTO updateWorkTaskDTO, 
        CancellationToken cancellationToken = default)
    {
        var sql =
           """
           Update public.work_task 
           SET status_id=@StatusId, assigned_user_id=@AssignedUserId, title=@Title, description=@Description 
           WHERE id=@Id
           """;

        var workTask = DTOToEntityMapper.ToEntity(updateWorkTaskDTO);
        Console.WriteLine($"Id={workTask.Id}, StatusId={workTask.StatusId}, AssignedUserId={workTask.AssignedUserId}, Title={workTask.Title}");

        var rowsAffected =
            await _dbService.EditDataAsync(
                sql,
                workTask,
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