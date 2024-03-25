using Domain.DeveloperNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.ProjectNS;
public class ProjectDeveloper
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid DeveloperId { get; set; }
}
