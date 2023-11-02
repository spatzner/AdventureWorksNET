using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventureWorks.Domain.Entities
{
    public class Person
    {
        public Person(
            PersonName name, 
            string personType, 
            List<string> emailAddress, 
            List<PhoneNumber>? phoneNumbers,
            List<Address>? addresses,
            XElement? additionalContactInfo, 
            XElement? demographics)
        {
            Name = name;
            PersonType = personType ?? throw new ArgumentNullException(nameof(personType));
            EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
            PhoneNumbers = phoneNumbers ?? new List<PhoneNumber>();
            Addresses = addresses ?? new List<Address>();
            AdditionalContactInfo = additionalContactInfo;
            Demographics = demographics;
        }

        public PersonName Name { get;  }

        public string PersonType { get;  }

        public List<string> EmailAddress { get; }

        public List<PhoneNumber> PhoneNumbers { get; }

        public List<Address> Addresses { get; }
        public XElement? AdditionalContactInfo { get; }

        public XElement? Demographics { get; }
    }
}
