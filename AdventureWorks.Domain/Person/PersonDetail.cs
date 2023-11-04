using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventureWorks.Domain.Person
{
    public class Person
    {
        public int? Id { get; }
        public PersonName Name { get; }
        public string PersonType { get; }

        public Person(
            int? id,
            PersonName name,
            string personType)
        {
            Id = id;
            Name = name;
            PersonType = personType ?? throw new ArgumentNullException(nameof(personType));
        }

        public Person(
            PersonName name,
            string personType)
            : this(null, name, personType)
        {
        }
    }

    public class PersonDetail : Person
    {
        public List<string> EmailAddresses { get; }

        public List<PhoneNumber> PhoneNumbers { get; }

        public List<Address> Addresses { get; }

        public XElement? AdditionalContactInfo { get; }

        public XElement? Demographics { get; }

        public PersonDetail(
            int? id,
            PersonName name,
            string personType,
            List<string> emailAddress,
            List<PhoneNumber>? phoneNumbers,
            List<Address>? addresses,
            XElement? additionalContactInfo,
            XElement? demographics)
            : base(id, name, personType)
        {
            EmailAddresses = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
            PhoneNumbers = phoneNumbers ?? new List<PhoneNumber>();
            Addresses = addresses ?? new List<Address>();
            AdditionalContactInfo = additionalContactInfo;
            Demographics = demographics;
        }

        public PersonDetail(
            PersonName name,
            string personType,
            List<string> emailAddress,
            List<PhoneNumber>? phoneNumbers,
            List<Address>? addresses,
            XElement? additionalContactInfo,
            XElement? demographics)
            : this(null, 
                name, 
                personType, 
                emailAddress, 
                phoneNumbers, 
                addresses, 
                additionalContactInfo, 
                demographics)
        {
        }

        public PersonDetail(
            Person person,
            List<string> emailAddress,
            List<PhoneNumber>? phoneNumbers,
            List<Address>? addresses,
            XElement? additionalContactInfo,
            XElement? demographics) 
            : this(person.Id, 
                person.Name, 
                person.PersonType, 
                emailAddress, 
                phoneNumbers, 
                addresses,
                additionalContactInfo,
                demographics)
        {
        }
    }
}