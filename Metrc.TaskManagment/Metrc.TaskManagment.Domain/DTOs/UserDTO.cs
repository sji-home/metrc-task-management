using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.DTOs;

public class UserDTO
{
    [MaxLength(255)]
    public required string UserName { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [MaxLength(255)]
    public required string Password { get; set; }

    public List<UserRoleDTO> AppUserRoles { get; set; } = [];
}
