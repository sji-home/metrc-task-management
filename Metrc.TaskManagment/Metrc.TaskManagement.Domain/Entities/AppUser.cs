using System.ComponentModel.DataAnnotations;

namespace Metrc.TaskManagement.Domain.Entities;

public class AppUser
{
    public AppUser() {}

    public int Id { get; set; }

    /// <summary>
    /// UserName can either be some unique string or can be the user's email address.
    /// </summary>
    [MaxLength(255)]
    public required string UserName { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [MaxLength(255)]
    public required string Password { get; set; }

    public IEnumerable<AppUserRole> AppUserRoles { get; set; } = [];
}
