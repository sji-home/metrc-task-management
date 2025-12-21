namespace Metrc.TaskManagement.Domain.DTOs;

public class UserRoleDTO
{
    public required AppRoleDTO Role { get; set; }
    public required int AppUserId { get; set; }
}
