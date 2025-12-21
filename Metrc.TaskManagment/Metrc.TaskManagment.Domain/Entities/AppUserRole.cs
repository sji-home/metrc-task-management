using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrc.TaskManagement.Domain.Entities;

public class AppUserRole
{
    public required AppRole Role { get; set; }
    public required int AppUserId { get; set; }
}
