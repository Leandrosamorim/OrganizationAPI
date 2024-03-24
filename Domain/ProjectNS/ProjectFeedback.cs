using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjectNS;
public class ProjectFeedback
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UId { get; set; }
    public Guid ProjectUId { get; set; }
    public Guid DeveloperUId { get; set; }
    public int Rating { get; set; }
    public string Message { get; set; }
}
