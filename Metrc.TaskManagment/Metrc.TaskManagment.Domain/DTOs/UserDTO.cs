using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Domain.DTOs;

public class UserDTO
{
    [MaxLength(255)]
    public required string UserName { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }
}
