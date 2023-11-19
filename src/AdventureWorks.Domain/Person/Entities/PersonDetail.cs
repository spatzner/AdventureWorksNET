using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventureWorks.Domain.Person.Entities
{
    public class PersonDetail : Person
    {
        public List<EmailAddress> EmailAddresses { get; init; } = new();

        public List<PhoneNumber> PhoneNumbers { get; init; } = new();

        public List<Address> Addresses { get; init; } = new();

    }
}