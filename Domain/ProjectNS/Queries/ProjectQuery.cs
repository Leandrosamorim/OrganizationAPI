using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS.Queries;
public class ProjectQuery
{
    public Guid OrganizationId { get; set; }
    public Guid DeveloperId { get; set; }
    public Guid ProjectId { get; set; }
}
