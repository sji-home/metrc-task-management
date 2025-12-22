using Metrc.TaskManagement.Domain.DTOs;
using Metrc.TaskManagement.Domain.Entities;

namespace Metrc.TaskManagement.Application.Profiles.Mappings;

public static class EntityToDTOMapper
{
    public static WorkTaskResponseDTO ToResponseDTO(this WorkTask workTask)
        => new WorkTaskResponseDTO
        {
            Id = workTask.Id,
            Status = workTask.Status,
            Title = workTask.Title,
            Description = workTask.Description,
            AssignedUserId = workTask.AssignedUserId
        };

    public static List<WorkTaskResponseDTO> ToResponseDTO(this IEnumerable<WorkTask> entities)
        => entities.Select(entity => entity.ToResponseDTO()).ToList();
}
