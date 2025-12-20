using Metrc.TaskManagment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Domain.Entities;

public class Task
{
    public int Id { get; set; }

    //public required int StatusId { get; set; }

    public required TaskStatusEnum Status { get; set; }

    public int? AssignedUserId { get; set; }

    [MaxLength(255)]
    public required string Description { get; set; }
}
