using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Domain.Entities;

public class TaskStatus
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Code { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }
}
