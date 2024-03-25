using Domain.ContactNS;
using Domain.ProjectNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UId { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public string StackName { get; set; }
    }
}
