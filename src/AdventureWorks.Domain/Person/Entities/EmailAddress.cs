using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities
{
    public class EmailAddress
    {
        public int? Id { get; set; }
        [MaxLength(50)]
        public required string Address { get; set; }
    }
}
