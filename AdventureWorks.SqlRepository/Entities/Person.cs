using System.Xml.Linq;

namespace AdventureWorks.SqlRepository.Entities
{
    public class Person
    {

        public int BusinessEntityId { get; set; }

        public string? PersonType { get; set; }

        public string? Title { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public XElement? AdditionalContactInfo { get; set; }

        public XElement? Demographics { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}