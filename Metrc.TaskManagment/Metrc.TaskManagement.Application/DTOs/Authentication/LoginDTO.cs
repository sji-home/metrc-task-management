using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Application.DTOs.Authentication;

public class LoginDTO
{
    [MaxLength(255)]
    public string UserName { get; set; } = string.Empty;


    [MaxLength(255)]
    public required string Password { get; set; } = string.Empty;
}
