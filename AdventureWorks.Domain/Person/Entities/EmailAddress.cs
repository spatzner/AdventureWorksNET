using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.Entities
{
    public class EmailAddress
    {
        public int? Id { get; set; }
        public required string Address { get; set; }
    }
}
