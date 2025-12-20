using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Domain.Entities;

public class AppUser
{
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }
}
