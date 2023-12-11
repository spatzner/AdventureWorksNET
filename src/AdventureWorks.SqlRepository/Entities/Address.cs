using AdventureWorks.Domain.Person.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.SqlRepository.Entities
{
    public class Address : Domain.Person.Entities.Address
    {
            public override string? Type { get; set; }
            public override string? Address1 { get; set; }
            public override string? Address2 { get; set; }
            public override string? City { get; set; }
            public override string? State { get; set; }
            public override string? Country { get; set; }
            public override string? PostalCode { get; set; }
    }
}
