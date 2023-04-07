using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrganizationNS.Queries
{
    public class OrganizationQuery
    {
        public List<Guid>? UId { get; set; }
        public List<string>? Name { get; set; }
    }
}
