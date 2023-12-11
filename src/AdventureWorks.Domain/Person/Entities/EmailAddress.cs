using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities
{
    public class EmailAddress :IValidatable
    {
        public int? Id { get; set; }

        public virtual string? Address { get; set; }
    }
}
