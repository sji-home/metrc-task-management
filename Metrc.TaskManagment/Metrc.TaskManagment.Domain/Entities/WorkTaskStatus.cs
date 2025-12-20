using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.Entities;

public class WorkTaskStatus
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Code { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }
}
