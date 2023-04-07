using Domain.ContactNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationNS
{
    public class OrganizationDTO
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
