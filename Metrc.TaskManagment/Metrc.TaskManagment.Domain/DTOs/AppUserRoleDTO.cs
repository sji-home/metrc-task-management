using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.DTOs;

public class AppUserRoleDTO
{
    public required int AppUserId { get; set; }

    [MaxLength(100)]
    public required string RoleName { get; set; } = string.Empty;
}
