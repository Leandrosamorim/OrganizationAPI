using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.ContactNS;

namespace Domain.OrganizationNS
{
    public class Organization
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UId { get; set; }
        public string Name { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
