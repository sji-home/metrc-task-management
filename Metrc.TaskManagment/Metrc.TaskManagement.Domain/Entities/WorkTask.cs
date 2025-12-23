using Metrc.TaskManagment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.Entities;

public class WorkTask
{
    public int Id { get; set; }

    public required int StatusId { get; set; }

    public int? AssignedUserId { get; set; }

    [MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(255)]
    public required string Description { get; set; }
}
