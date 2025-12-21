using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.DTOs;

public class RoleDTO
{
    [MaxLength(100)]
    public required string Name { get; set; }
}
