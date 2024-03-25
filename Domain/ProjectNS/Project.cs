using Domain.DeveloperNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS;
public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public ICollection<ProjectDeveloper> Developers { get; init; } = new HashSet<ProjectDeveloper>();
    public Guid OrganizationId { get; set; }
    public ICollection<ProjectFeedback> Feedbacks { get; set; } = new List<ProjectFeedback>();
}
