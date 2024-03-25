using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS.Commands;
public class CreateProjectCommand
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
}
