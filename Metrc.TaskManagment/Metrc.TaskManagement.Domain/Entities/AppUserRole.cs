using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.Entities;

public class AppUserRole
{
    public required int AppUserId { get; set; }

    [MaxLength(100)]
    public required string RoleName { get; set; } = string.Empty;

}
