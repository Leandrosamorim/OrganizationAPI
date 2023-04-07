using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MatchNS
{
    public class Match
    {
        public Guid OrganizationUId { get; set; }
        public Guid DeveloperUId { get; set; }
        public DateTime Date { get; set; }
    }
}
