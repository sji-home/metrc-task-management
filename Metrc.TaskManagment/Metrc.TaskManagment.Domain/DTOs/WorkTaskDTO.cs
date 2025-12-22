using Metrc.TaskManagment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.DTOs;

public class WorkTaskDTO
{
    public required WorkTaskStatusEnum Status { get; set; }

    public int? AssignedUserId { get; set; }

    [MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public required string Description { get; set; }
}
