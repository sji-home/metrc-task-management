using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Application.Profiles.Mappings;

public static class DTOToEntityMapper
{
    public static WorkTask ToEntity(this WorkTaskDTO workTaskDTO)
        => new WorkTask
        {
            StatusId = (int)workTaskDTO.Status,
            Title = workTaskDTO.Title,
            Description = workTaskDTO.Description,
            AssignedUserId = workTaskDTO.AssignedUserId
        };

    public static WorkTask ToEntity(this UpdateWorkTaskDTO updateWorkTaskDTO)
        => new WorkTask
        {
            Id = updateWorkTaskDTO.Id,
            StatusId = (int)updateWorkTaskDTO.Status,
            Title = updateWorkTaskDTO.Title,
            Description = updateWorkTaskDTO.Description,
            AssignedUserId = updateWorkTaskDTO.AssignedUserId
        };

    public static WorkTask ToEntity(this WorkTaskResponseDTO dto)
        => new WorkTask
        {
            Id = dto.Id,
            StatusId = (int)dto.Status,
            Title = dto.Title,
            Description = dto.Description,
            AssignedUserId = dto.AssignedUserId
        };

    public static List<WorkTask> ToEntity(this IEnumerable<WorkTaskResponseDTO> dtos)
        => dtos.Select(dto => dto.ToEntity()).ToList();
}
