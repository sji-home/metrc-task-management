using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.Entities;

public class Role
{
    [MaxLength(100)]
    public required string Name { get; set; }
}
