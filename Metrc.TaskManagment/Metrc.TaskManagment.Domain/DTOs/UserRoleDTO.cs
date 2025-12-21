namespace Metrc.TaskManagement.Domain.DTOs;

public class UserRoleDTO
{
    public required RoleDTO Role { get; set; }
    public required int AppUserId { get; set; }
}
